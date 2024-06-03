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

namespace HolyQuran.Infrastructure.Services.TajweedRules
{
    public class TajweedRuleService : ITajweedRuleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
         public TajweedRuleService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.TajweedRules.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<TajweedRule> query = _dbContext.TajweedRules
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<TajweedRuleViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<int> Create(CreateTajweedRuleDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<TajweedRule>(dto);   
            await _dbContext.TajweedRules.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateTajweedRuleDto dto)
        {
            var model = await _dbContext.TajweedRules
                .SingleOrDefaultAsync(x =>x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            
            var updatedmodel = _mapper.Map<UpdateTajweedRuleDto, TajweedRule>(dto, model);
            try
            {
                _dbContext.TajweedRules.Update(updatedmodel);
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
            var model = await _dbContext.TajweedRules.SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.TajweedRules.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }      
        public async Task<TajweedRuleViewModel> Get(int Id)
        {
            var model = await _dbContext.TajweedRules
                 .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<TajweedRuleViewModel>(model);
            return modelViewModel;
        }

    }
}
