using AutoMapper;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.ViewModel.Paginations;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HolyQuran.infrastructure.Extentions
{
    public static class PagingExtension
	{
		public static async Task<PagingResultViewModel<TViewModel>> ToPagedData<TViewModel>(this IQueryable<BaseEntity> query, Pagination dto, IMapper mapper)
			where TViewModel : IBaseViewModel
		{
			var pageSize = dto.PerPage;
			var skip = (int)Math.Ceiling(pageSize * (decimal)(dto.Page - 1));

			var totalCount = await query.CountAsync();
			query = query.Skip(skip).Take(pageSize);

			return new PagingResultViewModel<TViewModel>
			{
				Meta = new MetaViewModel
				{
					Page = dto.Page,
					Perpage = dto.PerPage,
					Total = totalCount
				},
				Data = mapper.Map<List<TViewModel>>(await query.ToListAsync())
			};
		}
	}
}
