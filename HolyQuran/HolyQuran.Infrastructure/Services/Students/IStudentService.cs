using Microsoft.AspNetCore.Identity;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.ViewModels;
using HolyQuran.Core.Enums;

namespace HolyQuran.Infrastructure.Services.Students
{
    public interface IStudentService
    {
        Task<StudentViewModel> Detailes(string? id);
        Task<IdentityResult> Create(CreateStudentDto dto);
        Task<IdentityResult> Update(UpdateStudentDto dto);
        Task<string> Delete(string ApplicationUserId);
        Task<StudentViewModel> Get(int Id);
        PagingAPIViewModel GetAll(int page);
        Task ChangeLevelType(int Id, LevelType level);
    }
}
