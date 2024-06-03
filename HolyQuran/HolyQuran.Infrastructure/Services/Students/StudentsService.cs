using HolyQuran.Core.Constants;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.Enums;
using HolyQuran.Core.Exceptions;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Data;
using HolyQuran.Data.Models;
using HolyQuran.Infrastructure.Services.Files;
using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

using System.Net.Mail;
using DocumentFormat.OpenXml.Spreadsheet;
using HolyQuran.Infrastructure.Services.Users;

namespace HolyQuran.Infrastructure.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        public StudentService(ApplicationDbContext db, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IFileService fileService, IUserService userService)
        {
            _dbContext = db;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _fileService = fileService;
            _userService = userService;
        }


        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Students
                .Include(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Student> query = _dbContext.Students
                .Include(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelmapper = _mapper.Map<List<StudentViewModel>>(modelquery);
            foreach (var item in modelmapper)
            {
                item.GenderType = _userService.ChangeLanguageGenderType(item.GenderType);
                item.UserType = _userService.ChangeLanguageUserType(item.UserType);
                item.LevelType = ChangeLanguageLevelType(item.LevelType);

            }
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelmapper,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public string ChangeLanguageLevelType(string levelType)
        {
            if (levelType == LevelType.Foundation.ToString())
            {
                levelType = "تأسيسي";
            }
            else if (levelType == LevelType.Supplementary.ToString())
            {
                levelType = "تكميلي";
            }
            else if (levelType == LevelType.Advanced.ToString())
            {
                levelType = "متقدم";
            }
            return levelType;
        }

        public async Task<StudentViewModel> Detailes(string? id)
        {
            var model = await _dbContext.Students
                .Include(x => x.ApplicationUser)
                .ThenInclude(x => x.Country)
                .SingleOrDefaultAsync(x => x.ApplicationUserId == id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            return _mapper.Map<StudentViewModel>(model);
        }
        public async Task<IdentityResult> Create(CreateStudentDto dto)
        {
            var emailOrPhoneIsExist = _dbContext.Students
                .Include(x => x.ApplicationUser)
                .Any(x => (x.ApplicationUser.Email == dto.Email ||
                x.ApplicationUser.PhoneNumber == dto.Phone));
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            // Create User
            string userId = await CreateUserForStudent(dto);
            // Create Student
            var student = new Student();
            student.ApplicationUserId = userId;
            student.LevelType = dto.LevelType;
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }
        public async Task ChangeLevelType(int Id, LevelType level)
        {
            var model = await _dbContext.Students.FindAsync(Id);

            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            model.LevelType = level;
            _dbContext.Students.Update(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<string> CreateUserForStudent(CreateStudentDto dto)
        {
            var country = await _dbContext.Countries.FindAsync(dto.CountryId);
            if (country is null)
            {
                throw new CountryItemNotFoundException();
            }
            // Create User
            var user = new ApplicationUser();
            user.FullName = dto.FullName;
            user.PhoneNumber = dto.Phone;
            user.DOB = dto.DOB;
            user.GenderType = dto.GenderType;
            user.UserType = UserType.Student;
            user.CountryId = dto.CountryId;
            user.Email = dto.Email;
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
          
                    //await _userManager.AddToRolesAsync(user, dto.UserType.ToString());
                await _userManager.AddToRoleAsync(user, UserType.Student.ToString());
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        throw new IOException(item.Description);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return user.Id;
        }
        public UpdateApplicationUserDto UpdatedUserModel(UpdateStudentDto dto)
        {
            var country =  _dbContext.Countries.Find(dto.CountryId);
            if (country is null)
            {
                throw new CountryItemNotFoundException();
            }
            // Update User
            var updatedUser = new UpdateApplicationUserDto();
            updatedUser.Id = dto.ApplicationUserId;
            updatedUser.FullName = dto.FullName;
            updatedUser.Phone = dto.Phone;
            updatedUser.DOB = dto.DOB;
            updatedUser.GenderType = dto.GenderType;
            updatedUser.CountryId = dto.CountryId;
            updatedUser.Email = dto.Email;
            updatedUser.Image = dto.Image;
            return updatedUser;
        }
        public async Task<IdentityResult> UpdateUserFromStudent(UpdateStudentDto dto)
        {
            var updatedUser = UpdatedUserModel(dto);
            var emailOrPhoneIsExist = _dbContext.Users
               .Any(x => (x.Email == dto.Email ||
               x.PhoneNumber == dto.Phone &&
               x.Id == dto.ApplicationUserId) &&
               x.Id != dto.ApplicationUserId);
            if (emailOrPhoneIsExist)
            {
                throw new DuplicateEmailOrPhoneException();
            }
            try
            {
                var user = await _dbContext.Users.FindAsync(updatedUser.Id);
                if (user != null)
                {
                    updatedUser.UserType = user.UserType;
                    var updatedUserMapper = _mapper.Map<UpdateApplicationUserDto, ApplicationUser>(updatedUser, user);
                    if (updatedUser.Image != null)
                    {
                        updatedUserMapper.Image = await _fileService.SaveFile(updatedUser.Image, FolderNames.ImagesFolder);
                    }
                    await _userManager.UpdateAsync(updatedUserMapper);
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IdentityResult> Update(UpdateStudentDto dto)
        { //Update User From Student
            var result = await UpdateUserFromStudent(dto);
            if (result.Succeeded)
            {
                var student = await _dbContext.Students.FindAsync(dto.Id);
                var updatedStudent = _mapper.Map<UpdateStudentDto, Student>(dto, student);
                try
                {
                    _dbContext.Students.Update(updatedStudent);
                    await _dbContext.SaveChangesAsync();
                    return IdentityResult.Success;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return IdentityResult.Failed();

        }
        public async Task<string> Delete(string ApplicationUserId)
        {
            var model = await _dbContext.Students
                .Include(x => x.ApplicationUser)
                .FirstAsync(x => x.ApplicationUserId == ApplicationUserId);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            try
            {
                _dbContext.Students.Update(model);
                await _dbContext.SaveChangesAsync();
                // Delete User 
                await _userService.Delete(ApplicationUserId);
            }
            catch (Exception)
            {

                throw;
            }

            return model.ApplicationUserId;
        }
        public async Task<StudentViewModel> Get(int Id)
        {
            var model = await _dbContext.Students
                         .Include(x => x.ApplicationUser)
                         .ThenInclude(x => x.Country)
                         .SingleOrDefaultAsync(x => x.Id == Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            var maper = _mapper.Map<StudentViewModel>(model);
            maper.GenderType = _userService.ChangeLanguageGenderType(maper.GenderType);
            maper.UserType = _userService.ChangeLanguageUserType(maper.UserType);
            maper.LevelType = ChangeLanguageLevelType(maper.LevelType);

            return maper;
        }
    }
}
