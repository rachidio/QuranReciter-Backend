using AutoMapper;
using HolyQuran.Data.Data;
using Microsoft.EntityFrameworkCore;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.Exceptions;
using HolyQuran.Core.ViewModels;
using HolyQuran.Core.Constants;
using HolyQuran.Infrastructure.Services.Files;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModel.Paginations;
using HolyQuran.Data.Models;
using HolyQuran.Core.Enums;

namespace HolyQuran.Infrastructure.Services.Recordings
{
    public class RecordingService : IRecordingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public RecordingService(ApplicationDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public PagingAPIViewModel GetAll(QueryDto filter)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Recordings.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                filter.Page = Math.Clamp(filter.Page, 1, totalPages);
            }
            var skipCount = (filter.Page - 1) * pageSize;

            IQueryable<Recording> query = _dbContext.Recordings
                .Include(x => x.Chapter)
                .Include(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);

            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<RecordingViewModel>>(modelquery);
            if (!string.IsNullOrWhiteSpace(filter.RecordingStatus.ToString()))
            {
                modelViewModels = modelViewModels.Where(x => x.RecordingStatus.Equals(filter.RecordingStatus.ToString())).ToList();
            }
            foreach (var item in modelViewModels)
            {
                item.RecordingStatus = ChangeLanguageRecordingStatus(item.RecordingStatus);
            }
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = filter.Page
            };

            return pagingResult;
        }
        public string ChangeLanguageRecordingStatus(string status)
        {
            if (status == RecordingStatus.DoneEvaluate.ToString())
            {
                status = "تم التقييم";
            }
            else if (status == RecordingStatus.NotEvaluateYet.ToString())
            {
                status = "لم يتم التفييم بغد";
            }
            else if (status == RecordingStatus.PendingEvaluation.ToString())
            {
                status = "جاري التقييم";
            }
            return status;

        }
        public async Task ChangeStatus(int Id, RecordingStatus status)
        {
            var model = await _dbContext.Recordings.FindAsync(Id);

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.RecordingStatus = status;
            _dbContext.Recordings.Update(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Create(CreateRecordingDto dto)
        {
            var chapter = await _dbContext.Chapters.FindAsync(dto.ChapterId);
            if (chapter is null)
            {
                throw new ChapterItemNotFoundException();
            }
            var student = await _dbContext.Students.FindAsync(dto.StudentId);
            if (student is null)
            {
                throw new StudentItemNotFoundException();
            }
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Recording>(dto);
            if (dto.File_path != null)
            {
                model.File_path = await _fileService.SaveFile(dto.File_path, FolderNames.RecordingsFolder);
            }
            await _dbContext.Recordings.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateRecordingDto dto)
        {
            var chapter = await _dbContext.Chapters.FindAsync(dto.ChapterId);
            if (chapter is null)
            {
                throw new ChapterItemNotFoundException();
            }
            var student = await _dbContext.Students.FindAsync(dto.StudentId);
            if (student is null)
            {
                throw new StudentItemNotFoundException();
            }
            var model = await _dbContext.Recordings
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var updatedmodel = _mapper.Map<UpdateRecordingDto, Recording>(dto, model);
            if (dto.File_path != null)
            {
                model.File_path = await _fileService.SaveFile(dto.File_path, FolderNames.RecordingsFolder);
            }
            try
            {
                _dbContext.Recordings.Update(updatedmodel);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }

            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _dbContext.Recordings.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.Recordings.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<RecordingViewModel> Get(int Id)
        {
            var model = await _dbContext.Recordings
                .Include(x => x.Chapter)
                .Include(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<RecordingViewModel>(model);
            modelViewModel.RecordingStatus = ChangeLanguageRecordingStatus(modelViewModel.RecordingStatus);
            return modelViewModel;
        }

    }
}
