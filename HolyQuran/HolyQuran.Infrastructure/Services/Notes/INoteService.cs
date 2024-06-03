using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.Notes
{
    public interface INoteService

    {
        PagingAPIViewModel GetAll(int page);
        Task<int> Create(CreateNoteDto dto);
        Task<int> Update(UpdateNoteDto dto);
        Task<int> Delete(int Id);
        Task<NoteViewModel> Get(int Id);
    }
}
