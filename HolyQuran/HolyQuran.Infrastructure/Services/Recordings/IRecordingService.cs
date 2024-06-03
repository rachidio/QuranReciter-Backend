using HolyQuran.Core.Dtos;
using HolyQuran.Core.Enums;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.Recordings
{
    public interface IRecordingService

    {
        PagingAPIViewModel GetAll(QueryDto filter);
        Task<int> Create(CreateRecordingDto dto);
        Task<int> Update(UpdateRecordingDto dto);
        Task<int> Delete(int Id);
        Task<RecordingViewModel> Get(int Id);
        Task ChangeStatus(int Id, RecordingStatus status);
       string ChangeLanguageRecordingStatus(string status);
    }
}
