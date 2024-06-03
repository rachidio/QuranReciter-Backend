using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.GeneralNotes
{
    public interface IGeneralNoteService

    {
        PagingAPIViewModel GetAll(int page);
        Task<int> Create(CreateGeneralNoteDto dto);
        Task<int> Update(UpdateGeneralNoteDto dto);
        Task<int> Delete(int Id);
        Task<UpdateGeneralNoteDto> Get(int Id);
        Task<bool> DeleteGeneralNotesByNoteId(int Id);
    }
}
