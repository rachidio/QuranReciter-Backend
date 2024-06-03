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
using DocumentFormat.OpenXml.ExtendedProperties;

namespace HolyQuran.Infrastructure.Services.GeneralNotes
{
    public class GeneralNoteService : IGeneralNoteService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public GeneralNoteService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.GeneralNotes.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<GeneralNote> query = _dbContext.GeneralNotes
                .Include(x => x.Note)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<GeneralNoteViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<int> Create(CreateGeneralNoteDto dto)
        {
            var note = await _dbContext.Notes.FindAsync(dto.NoteId);
            if (note is null)
            {
                throw new NoteItemNotFoundException();
            }
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<GeneralNote>(dto);
            await _dbContext.GeneralNotes.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateGeneralNoteDto dto)
        {
            var note = await _dbContext.Notes.FindAsync(dto.NoteId);
            if (note is null)
            {
                throw new NoteItemNotFoundException();
            }
            var model = await _dbContext.GeneralNotes
                .SingleOrDefaultAsync(x =>x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }          
            var updatedmodel = _mapper.Map<UpdateGeneralNoteDto, GeneralNote>(dto, model);
            try
            {
                _dbContext.GeneralNotes.Update(updatedmodel);
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
            var model = await _dbContext.GeneralNotes.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.GeneralNotes.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<bool> DeleteGeneralNotesByNoteId(int Id)
        {
            var model = await _dbContext.GeneralNotes.Where(x => x.NoteId == Id).ToListAsync();
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            foreach (var item in model)
            {
                item.IsDelete = true;

            }
            _dbContext.GeneralNotes.UpdateRange(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<UpdateGeneralNoteDto> Get(int Id)
        {
            var model = await _dbContext.GeneralNotes
                .Include(x => x.Note)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var modelViewModel = _mapper.Map<UpdateGeneralNoteDto>(model);
            return modelViewModel;
        }

    }
}
