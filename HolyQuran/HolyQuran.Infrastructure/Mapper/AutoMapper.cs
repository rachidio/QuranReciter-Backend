using HolyQuran.Core.Dtos;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using HolyQuran.Data.Models;
using AutoMapper;


namespace HolyQuran.infrastructure.Mapper
{

    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //user
            CreateMap<ApplicationUser, ApplicationUserViewModel>()
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.PhoneNumber))
                .ForMember(x => x.Country, x => x.MapFrom(x => x.Country.NameAr))
                .ForMember(x => x.DOB, x => x.MapFrom(x => x.DOB.ToString("yyyy:MM:dd")))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateApplicationUserDto, ApplicationUser>()
                .ForMember(src => src.Image, opt => opt.Ignore());
            CreateMap<UpdateApplicationUserDto, ApplicationUser>()
                .ForMember(src => src.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.PhoneNumber))
                .ForMember(src => src.Image, opt => opt.Ignore());

            //Student
            CreateMap<Student, StudentViewModel>()
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Country, x => x.MapFrom(x => x.ApplicationUser.Country.NameAr))
                .ForMember(x => x.Email, x => x.MapFrom(x => x.ApplicationUser.Email))
                .ForMember(x => x.Username, x => x.MapFrom(x => x.ApplicationUser.UserName))
                .ForMember(x => x.DOB, x => x.MapFrom(x => x.ApplicationUser.DOB))
                .ForMember(x => x.FullName, x => x.MapFrom(x => x.ApplicationUser.FullName))
                .ForMember(x => x.Image, x => x.MapFrom(x => x.ApplicationUser.Image))
                .ForMember(x => x.UserType, x => x.MapFrom(x => x.ApplicationUser.UserType.ToString()))
                .ForMember(x => x.GenderType, x => x.MapFrom(x => x.ApplicationUser.GenderType.ToString()))
                .ForMember(x => x.DOB, x => x.MapFrom(x => x.ApplicationUser.DOB.ToString("yyyy:MM:dd")))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateStudentDto, Student>();
            CreateMap<UpdateStudentDto, Student>()
               .ReverseMap();
            //Teacher
            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(x => x.Phone, x => x.MapFrom(x => x.ApplicationUser.PhoneNumber))
                .ForMember(x => x.Country, x => x.MapFrom(x => x.ApplicationUser.Country.NameAr))
                .ForMember(x => x.Email, x => x.MapFrom(x => x.ApplicationUser.Email))
                .ForMember(x => x.Username, x => x.MapFrom(x => x.ApplicationUser.UserName))
                .ForMember(x => x.DOB, x => x.MapFrom(x => x.ApplicationUser.DOB))
                .ForMember(x => x.FullName, x => x.MapFrom(x => x.ApplicationUser.FullName))
                .ForMember(x => x.Image, x => x.MapFrom(x => x.ApplicationUser.Image))
                .ForMember(x => x.UserType, x => x.MapFrom(x => x.ApplicationUser.UserType.ToString()))
                .ForMember(x => x.GenderType, x => x.MapFrom(x => x.ApplicationUser.GenderType.ToString()))
                .ForMember(x => x.ApplicationUserId, x => x.MapFrom(x => x.ApplicationUser.Id.ToString()))
                .ForMember(x => x.DOB, x => x.MapFrom(x => x.ApplicationUser.DOB.ToString("yyyy:MM:dd")))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));

            CreateMap<CreateTeacherDto, Teacher>();
            CreateMap<UpdateTeacherDto, Teacher>()
               .ReverseMap();
            //Recording
            CreateMap<Recording, RecordingViewModel>()
                .ForMember(x => x.Chapter, x => x.MapFrom(x => x.Chapter.Arabic))
                .ForMember(x => x.RecordingStatus, x => x.MapFrom(x => x.RecordingStatus.ToString()))
                .ForMember(x => x.Student, x => x.MapFrom(x => x.Student.ApplicationUser.FullName))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateRecordingDto, Recording>();
            CreateMap<UpdateRecordingDto, Recording>()
               .ReverseMap();

            //Evaluation
            CreateMap<Evaluation, EvaluationViewModel>()
                .ForMember(x => x.Teacher, x => x.MapFrom(x => x.Teacher.ApplicationUser.FullName))
                .ForMember(x => x.SumbitStatus, x => x.MapFrom(x => x.SumbitStatus.ToString()))
                //.ForMember(x => x.StatusType, x => x.MapFrom(x => x.StatusType.ToString()))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateEvaluationDto, Evaluation>();
            CreateMap<UpdateEvaluationDto, Evaluation>()
               .ReverseMap();

            //Note
            CreateMap<Note, NoteViewModel>()
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")))
                .ForMember(x => x.Evaluation, x => x.MapFrom(x => x.Evaluation.Id));

            CreateMap<CreateNoteDto, Note>();
            CreateMap<UpdateNoteDto, Note>()
               .ReverseMap();

            //Specific Note
            CreateMap<SpecificNote, SpecificNoteViewModel>()
                .ForMember(x => x.Note, x => x.MapFrom(x => x.Note.Id))
                .ForMember(x => x.TimeSpecificNote, x => x.MapFrom(x => x.TimeSpecificNote.ToString("HH:mm")))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateSpecificNoteDto, SpecificNote>();
            CreateMap<UpdateSpecificNoteDto, SpecificNote>()
               .ReverseMap();

            //General Note
            CreateMap<GeneralNote, GeneralNoteViewModel>()
                .ForMember(x => x.Note, x => x.MapFrom(x => x.Note.Id))
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateGeneralNoteDto, GeneralNote>();
            CreateMap<UpdateGeneralNoteDto, GeneralNote>()
               .ReverseMap();
            //Tajweed Rule
            CreateMap<TajweedRule, TajweedRuleViewModel>()
                .ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy:MM:dd")));
            CreateMap<CreateTajweedRuleDto, TajweedRule>();
            CreateMap<UpdateTajweedRuleDto, TajweedRule>()
               .ReverseMap();

            //Country
            CreateMap<Country, CountryViewModel>();
            //Country
            CreateMap<Chapter, ChapterViewModel>();
        }
    }
}
