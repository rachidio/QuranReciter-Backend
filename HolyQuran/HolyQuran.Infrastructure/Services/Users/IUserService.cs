using Microsoft.AspNetCore.Identity;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.ViewModels;
 
namespace HolyQuran.Infrastructure.Services.Users
{
    public interface IUserService
    {
        //Task<List<UserViewModel>> GetAll(string? GeneralSearch);
         Task<ApplicationUserViewModel> Detailes(string? id);
        Task<IdentityResult> Create(CreateApplicationUserDto dto);
        Task<IdentityResult> Update(UpdateApplicationUserDto dto);
        Task<string> Delete(string Id);
        Task<ApplicationUserViewModel> Get(string Id);
        Task<bool> EmailConfirmed(string Id, int code);
        Task<SignInResult> Login(string UserName, string Password);
        Task<ApplicationUserViewModel> GetUserByUserName(string UserName);
        PagingAPIViewModel GetAll(int page);
        public string ChangeLanguageGenderType(string genderType);
        public string ChangeLanguageUserType(string userType);
    }
}
