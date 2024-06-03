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
using HolyQuran.Infrastructure.Services.GeneralNotes;
using HolyQuran.Infrastructure.Services.SpecificNotes;
using HolyQuran.Infrastructure.Services.Evaluations;

namespace HolyQuran.Infrastructure.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IGeneralNoteService _generalNoteService;
        private readonly ISpecificNoteService _specificNoteService;
        private readonly IEvaluationService _evaluationService ;

        public NoteService(
            ApplicationDbContext dbContext,
            IMapper mapper,
            IGeneralNoteService generalNoteService,
            ISpecificNoteService specificNoteService,
            IEvaluationService evaluationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _generalNoteService = generalNoteService;
            _specificNoteService = specificNoteService;
            _evaluationService = evaluationService;
        }
       
        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Notes.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Note> query = _dbContext.Notes
                .Include(x => x.Evaluation)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x=>x.ApplicationUser)
                .Include(x=>x.Evaluation.Recording)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x=>x.Country)
                .Include(x => x.Evaluation.Recording.Chapter)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<NoteViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<int> Create(CreateNoteDto dto)
        {
            var evaluation = await _dbContext.Evaluations.FindAsync(dto.EvaluationId);
            if (evaluation is null)
            {
                throw new EvaluationItemNotFoundException();
            }      
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Note>(dto);
            //Check If Evaluation Is Aleardy Exist 
            await _dbContext.Notes.AddAsync(model);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception message)
            {
                if (message.InnerException.ToString().Contains("duplicate key"))
                {
                    throw new DuplicateEvaluationInSameNoteException();
                }
                else
                {
                    throw message;
                }
            }
            return model.Id;
        }
        public async Task<int> Update(UpdateNoteDto dto)
        {
            var evaluation = await _dbContext.Evaluations.FindAsync(dto.EvaluationId);
            if (evaluation is null)
            {
                throw new EvaluationItemNotFoundException();
            }
           
            var model = await _dbContext.Notes
                .SingleOrDefaultAsync(x =>x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }          
            var updatedmodel = _mapper.Map<UpdateNoteDto, Note>(dto, model);
            try
            {
                _dbContext.Notes.Update(updatedmodel);
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
            var model = await _dbContext.Notes.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.Notes.Update(model);
            // Delete Genral Note 
            await _generalNoteService.DeleteGeneralNotesByNoteId(model.Id);
            // Delete Specific Note 
            await _specificNoteService.DeleteSpecificNotesByNoteId(model.Id);
            // Delete Specific Note 
            await _evaluationService.Delete(model.EvaluationId);
            _dbContext.SaveChanges();
            return model.Id;
        }      
        public async Task<NoteViewModel> Get(int Id)
        {
            var model = await _dbContext.Notes
                .Include(x => x.Evaluation)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.ApplicationUser)
                .Include(x => x.Evaluation.Recording)
                .ThenInclude(x => x.Student)
                .ThenInclude(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .Include(x => x.Evaluation.Recording.Chapter)
                .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var modelViewModel = _mapper.Map<NoteViewModel>(model);
            return modelViewModel;
        }

    }
}
