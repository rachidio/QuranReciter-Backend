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
using DocumentFormat.OpenXml.EMMA;

namespace HolyQuran.Infrastructure.Services.SpecificNotes
{
    public class SpecificNoteService : ISpecificNoteService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public SpecificNoteService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.SpecificNotes.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<SpecificNote> query = _dbContext.SpecificNotes
                .Include(x => x.Note)
                .Include(x => x.TajweedRule)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<SpecificNoteViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<int> Create(CreateSpecificNoteDto dto)
        {
            var note = await _dbContext.Notes.FindAsync(dto.NoteId);
            if (note is null)
            {
                throw new NoteItemNotFoundException();
            }
            var tajweedRule = _dbContext.TajweedRules.OrderBy(x => x.CreatedAt).FirstOrDefault<TajweedRule>();
            if (tajweedRule is null)
            {
                throw new TajweedRuleItemNotFoundException();
            }

            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<SpecificNote>(dto);
            await _dbContext.SpecificNotes.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateSpecificNoteDto dto)
        {
            var note = await _dbContext.Notes.FindAsync(dto.NoteId);
            if (note is null)
            {
                throw new NoteItemNotFoundException();
            }
            var tajweedRule = await _dbContext.TajweedRules.FindAsync(dto.TajweedRuleId);
            if (tajweedRule is null)
            {
                throw new TajweedRuleItemNotFoundException();
            }
            var model = await _dbContext.SpecificNotes
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var updatedmodel = _mapper.Map<UpdateSpecificNoteDto, SpecificNote>(dto, model);
            try
            {
                _dbContext.SpecificNotes.Update(updatedmodel);
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
            var model = await _dbContext.SpecificNotes.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.SpecificNotes.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<bool> DeleteSpecificNotesByNoteId(int Id)
        {
            var model = await _dbContext.SpecificNotes.Where(x => x.NoteId == Id).ToListAsync();
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            foreach (var item in model)
            {
                item.IsDelete = true;

            }
            _dbContext.SpecificNotes.UpdateRange(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<UpdateSpecificNoteDto> Get(int Id)
        {
            var model = await _dbContext.SpecificNotes
                .Include(x => x.Note)
                .Include(x => x.TajweedRule)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var modelViewModel = _mapper.Map<UpdateSpecificNoteDto>(model);
            return modelViewModel;
        }

    }
}
