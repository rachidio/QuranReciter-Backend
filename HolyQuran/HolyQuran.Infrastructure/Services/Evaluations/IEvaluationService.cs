using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.Evaluations
{
    public interface IEvaluationService

    {
        PagingAPIViewModel GetAll(QueryDto filter);
        Task<int> Create(CreateEvaluationDto dto);
        Task<int> Update(UpdateEvaluationDto dto);
        Task<int> Delete(int Id);
        Task<EvaluationViewModel> Get(int Id);
        Task ChangeSumbitStatus(int Id);
    }
}
