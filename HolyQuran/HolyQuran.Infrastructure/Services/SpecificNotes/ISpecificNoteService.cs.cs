using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.SpecificNotes
{
    public interface ISpecificNoteService

    {
        PagingAPIViewModel GetAll(int page);
        Task<int> Create(CreateSpecificNoteDto dto);
        Task<int> Update(UpdateSpecificNoteDto dto);
        Task<int> Delete(int Id);
        Task<UpdateSpecificNoteDto> Get(int Id);
        Task<bool> DeleteSpecificNotesByNoteId(int Id);
    }
}
