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
using HolyQuran.Data.Models;
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Identity;
using HolyQuran.Infrastructure.Services.Recordings;
using Humanizer;
using HolyQuran.infrastructure.Extentions;
using HolyQuran.Core.ViewModel.Paginations;
using Microsoft.AspNetCore.Http;

namespace HolyQuran.Infrastructure.Services.Evaluations
{
    public class EvaluationService : IEvaluationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRecordingService _recordingService;

        public EvaluationService(ApplicationDbContext dbContext, IMapper mapper, IRecordingService recordingService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _recordingService = recordingService;
        }


 

    public PagingAPIViewModel GetAll(QueryDto filter)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Evaluations.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                filter.Page = Math.Clamp(filter.Page, 1, totalPages);
            }
            var skipCount = (filter.Page - 1) * pageSize;

            IQueryable<Evaluation> query = _dbContext.Evaluations
                .Include(x => x.Teacher)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Recording)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .Include(X => X.Recording.Chapter)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);

            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<EvaluationViewModel>>(modelquery);

            if (!string.IsNullOrWhiteSpace(filter.SumbitStatus.ToString()))
            {
                modelViewModels = modelViewModels.Where(x => x.SumbitStatus.Equals(filter.SumbitStatus.ToString())).ToList();
            }
            foreach (var item in modelViewModels)
            {
                item.SumbitStatus = ChangeLanguageSumbitStatus(item.SumbitStatus);
                item.Recording.RecordingStatus = _recordingService.ChangeLanguageRecordingStatus(item.Recording.RecordingStatus);
            }
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = filter.Page
            };

            return pagingResult;
        }
        public string ChangeLanguageSumbitStatus(string status)
        {
            if (status == SumbitStatus.Save.ToString())
            {
                status = "حفظ";
            }
            else if (status == SumbitStatus.Send.ToString())
            {
                status = "ارسال";
            }
            return status;

        }
        public async Task ChangeSumbitStatus(int Id)
        {
            var model = await _dbContext.Evaluations.FindAsync(Id);

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.SumbitStatus = SumbitStatus.Send;
            _dbContext.Evaluations.Update(model);
            await _dbContext.SaveChangesAsync();
            // change status to Done
            await _recordingService.ChangeStatus(model.RecordingId, RecordingStatus.DoneEvaluate);
        }
        public async Task<int> Create(CreateEvaluationDto dto)
        {
            var teacher = await _dbContext.Teachers.FindAsync(dto.TeacherId);
            if (teacher is null)
            {
                throw new TeacherItemNotFoundException();
            }
            var recording = await _dbContext.Recordings.FindAsync(dto.RecordingId);
            if (recording is null)
            {
                throw new RecordingItemNotFoundException();
            }
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Evaluation>(dto);
            await _dbContext.Evaluations.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            // change status to pending
            await _recordingService.ChangeStatus(dto.RecordingId, RecordingStatus.PendingEvaluation);
            return model.Id;
        }
        public async Task<int> Update(UpdateEvaluationDto dto)
        {
            var teacher = await _dbContext.Teachers.FindAsync(dto.TeacherId);
            if (teacher is null)
            {
                throw new TeacherItemNotFoundException();
            }
            var recording = await _dbContext.Recordings.FindAsync(dto.RecordingId);
            if (recording is null)
            {
                throw new RecordingItemNotFoundException();
            }
            var model = await _dbContext.Evaluations
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedmodel = _mapper.Map<UpdateEvaluationDto, Evaluation>(dto, model);
            try
            {
                _dbContext.Evaluations.Update(updatedmodel);
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
            var model = await _dbContext.Evaluations.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
           // model.IsDelete = true;
            _dbContext.Evaluations.Remove(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<EvaluationViewModel> Get(int Id)
        {
            var model = await _dbContext.Evaluations
                .Include(x => x.Teacher)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Recording)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .Include(X => X.Recording.Chapter)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<EvaluationViewModel>(model);
            modelViewModel.SumbitStatus = ChangeLanguageSumbitStatus(modelViewModel.SumbitStatus);
            modelViewModel.Recording.RecordingStatus = _recordingService.ChangeLanguageRecordingStatus(modelViewModel.Recording.RecordingStatus);

            return modelViewModel;
        }

    }
}
