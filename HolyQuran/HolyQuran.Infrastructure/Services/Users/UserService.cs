using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.Enums;
using HolyQuran.Core.Exceptions;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Data;
using HolyQuran.Data.Models;
using HolyQuran.Infrastructure.Services.Files;
using static HolyQuran.infrastructure.Mapper.AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

using System.Net.Mail;
using AutoMapper;

namespace HolyQuran.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFileService _fileService;

        public UserService(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IFileService fileService)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _fileService = fileService;
        }
        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _userManager.Users.Include(x => x.Country).Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<ApplicationUser> query = _userManager.Users
                .Include(x => x.Country)
                .Where(x => x.UserType == UserType.Admin)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);

            var modelquery = query.ToList();
            var modelmapper = _mapper.Map<List<ApplicationUserViewModel>>(modelquery);
            foreach (var item in modelmapper)
            {
                item.GenderType = ChangeLanguageGenderType(item.GenderType);
                item.UserType = ChangeLanguageUserType(item.UserType);
            }
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelmapper,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public string ChangeLanguageGenderType(string genderType)
        {
            if (genderType == GenderType.Female.ToString())
            {
                genderType = "أنثى";
            }
            else if (genderType == GenderType.Male.ToString())
            {
                genderType = "ذكر";
            }
            return genderType;

        }
        public string ChangeLanguageUserType(string userType)
        {
            if (userType == UserType.Admin.ToString())
            {
                userType = "مدير النظام";
            }
            else if (userType == UserType.Teacher.ToString())
            {
                userType = "معلم";
            }
            else if (userType == UserType.Student.ToString())
            {
                userType = "طالب";
            }
            return userType;

        }
        public async Task<ApplicationUserViewModel> Detailes(string? id)
        {
            var user = await _db.Users
                .Where(x => x.UserType == UserType.Admin)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<ApplicationUserViewModel>(user);
        }
        public async Task<IdentityResult> Create(CreateApplicationUserDto dto)
        {
            var country = await _db.Countries.FindAsync(dto.CountryId);
            if (country is null)
            {
                throw new CountryItemNotFoundException();
            }
            var emailOrPhoneIsExist = _db.Users
                .Any(x => (x.Email == dto.Email || x.PhoneNumber == dto.Phone));
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            var user = _mapper.Map<ApplicationUser>(dto);
            user.PhoneNumber = dto.Phone;
            user.CountryId = dto.CountryId;
            if (dto.Image != null)
            {
                user.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            user.UserName = dto.Email;
            try
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (dto.UserType != null)
                {
                    //await _userManager.AddToRolesAsync(user, dto.UserType.ToString());
                    await _userManager.AddToRoleAsync(user, dto.UserType.ToString());
                }
                if (!result.Succeeded)
                {
                    throw new OperationFailedException();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //await _emailService.Send(user.Email, "New Account !", $"Username is : {user.Email} and Password is { password }");
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> Update(UpdateApplicationUserDto dto)
        {
            var country = await _db.Countries.FindAsync(dto.CountryId);
            if (country is null)
            {
                throw new CountryItemNotFoundException();
            }
            var emailOrPhoneIsExist = _db.Users
                .Any(x => (x.Email == dto.Email ||
                x.PhoneNumber == dto.Phone &&
                x.Id == dto.Id) &&
                x.Id != dto.Id);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            try
            {
                var user = _db.Users.Find(dto.Id);
                var currentUserRole = user.UserType.ToString();
                var updatedUser = _mapper.Map<UpdateApplicationUserDto, ApplicationUser>(dto, user);
                if (dto.Image != null)
                {
                    updatedUser.Image = await _fileService.SaveFile(dto.Image, Constant.Images);
                }
                await _userManager.UpdateAsync(updatedUser);
                if (!currentUserRole.Equals(dto.UserType.ToString()))
                {
                    await _userManager.RemoveFromRoleAsync(user, currentUserRole);
                    await _userManager.AddToRoleAsync(user, user.UserType.ToString());
                }
                return IdentityResult.Success;
            }
            catch (Exception)
            {
                throw;
            }


        }
        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return null;
        }
        public async Task<ApplicationUserViewModel> Get(string Id)
        {
            var user = await _db.Users
                .Include(x => x.Country)
                .SingleOrDefaultAsync(x =>
                x.Id == Id &&
                x.UserType == UserType.Admin);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            var modelmapper = _mapper.Map<ApplicationUserViewModel>(user);
            return modelmapper;
        }
        public async Task<ApplicationUserViewModel> GetUserByUserName(string UserName)
        {
            var user = await _db.Users
                .SingleOrDefaultAsync(x =>
                (x.UserName == UserName || x.Email == UserName) &&
                (x.UserType == UserType.Admin)
                );
            if (user == null)
            {
                return null;
            }
            var mapper = _mapper.Map<ApplicationUserViewModel>(user);
            return mapper;
        }
        public async Task<bool> EmailConfirmed(string Id, int code)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id);
            if (user == null)
            {
                throw new EntityNotFoundException();
            }
            if (user.Code == code)
            {
                user.EmailConfirmed = true;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<SignInResult> Login(string UserName, string Password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => (x.UserName == UserName || x.Email == UserName));

            if (user == null)
            {
                return SignInResult.Failed;
            }
            var result = await _userManager.CheckPasswordAsync(user, Password);

            if (!result)
            {
                return SignInResult.Failed;
            }

            return SignInResult.Success;
        }
    }
}
