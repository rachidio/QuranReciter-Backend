using Microsoft.AspNetCore.Identity;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.ViewModels;
 
namespace HolyQuran.Infrastructure.Services.Teachers
{
    public interface ITeacherService
    {
        //Task<List<UserViewModel>> GetAll(string? GeneralSearch);
        Task<TeacherViewModel> Detailes(string? id);
        Task<IdentityResult> Create(CreateTeacherDto dto);
        Task<IdentityResult> Update(UpdateTeacherDto dto);
        Task<string> Delete(string ApplicationUserId);
        Task<TeacherViewModel> Get(int Id);
        PagingAPIViewModel GetAll(int page);
    }
}
