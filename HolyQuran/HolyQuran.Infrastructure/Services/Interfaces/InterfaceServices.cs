using HolyQuran.Data.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using HolyQuran.Infrastructure.Services.Files;
using HolyQuran.Infrastructure.Services.Users;
using HolyQuran.Data.Models;
using HolyQuran.Infrastructure.Services.Recordings;
using HolyQuran.Infrastructure.Services.Evaluations;
using HolyQuran.Infrastructure.Services.Notes;
using HolyQuran.Infrastructure.Services.TajweedRules;
using HolyQuran.Infrastructure.Services.Teachers;
using HolyQuran.Infrastructure.Services.Students;
using HolyQuran.Infrastructure.Services.SpecificNotes;
using HolyQuran.Infrastructure.Services.GeneralNotes;
using HolyQuran.Infrastructure.Services.Countries;
using HolyQuran.Infrastructure.Services.Chapters;

namespace HolyQuran.Infrastructure.Services.Interfaces
{
    public class InterfaceServices : IInterfaceServices
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public InterfaceServices(
            IMapper mapper,
            ApplicationDbContext db,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> user_Manager,
            RoleManager<IdentityRole> role_Manager,
            SignInManager<ApplicationUser> signIn_Manager
            )
        {

            _mapper = mapper;
            _db = db;
            _env = env;
            _userManager = user_Manager;
            _roleManager = role_Manager;
            _signInManager = signIn_Manager;
            fileService = new FileService(_env);
            userService = new UserService(_db, _mapper, _userManager, _roleManager, fileService);
            studentService = new StudentService(_db, _mapper, _userManager, _roleManager, fileService, userService);
            teacherService = new TeacherService(_db, _mapper, _userManager, _roleManager, fileService, userService);
            recordingService = new RecordingService(_db, _mapper, fileService);
            evaluationService = new EvaluationService(_db, _mapper,recordingService);
            tajweedRuleService = new TajweedRuleService(_db, _mapper);
            specificNoteService = new SpecificNoteService(_db, _mapper);
            generalNoteService = new GeneralNoteService(_db, _mapper);
            noteService = new NoteService(_db, _mapper, generalNoteService, specificNoteService,evaluationService);
            countriesService = new CountriesService(_db, _mapper);
            chapterService = new ChapterService(_db, _mapper);

        }

        public IFileService fileService { get; private set; }
        public IUserService userService { get; private set; }
        public IStudentService studentService { get; private set; }
        public ITeacherService teacherService { get; private set; }
        public IRecordingService recordingService { get; private set; }
        public IEvaluationService evaluationService { get; private set; }
        public INoteService noteService { get; private set; }
        public ITajweedRuleService  tajweedRuleService { get; private set; }
        public ISpecificNoteService specificNoteService { get; private set; }
        public IGeneralNoteService generalNoteService { get; private set; }
        public ICountriesService countriesService { get; private set; }
        public IChapterService chapterService { get; private set; }

    }
}
