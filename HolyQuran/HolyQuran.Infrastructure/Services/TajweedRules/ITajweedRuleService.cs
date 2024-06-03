using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.TajweedRules
{
    public interface ITajweedRuleService

    {
        PagingAPIViewModel GetAll(int page);
        Task<int> Create(CreateTajweedRuleDto dto);
        Task<int> Update(UpdateTajweedRuleDto dto);
        Task<int> Delete(int Id);
        Task<TajweedRuleViewModel> Get(int Id);
    }
}
