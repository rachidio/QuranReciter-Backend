 using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using HolyQuran.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolyQuran.Infrastructure.Services.Files;
using HolyQuran.Data.Models;
using HolyQuran.Infrastructure.Services.Recordings;
using HolyQuran.Infrastructure.Services.Evaluations;
using HolyQuran.Infrastructure.Services.Users;
using HolyQuran.Infrastructure.Services.Students;
using HolyQuran.Infrastructure.Services.Teachers;
using HolyQuran.Infrastructure.Services.Notes;
using HolyQuran.Infrastructure.Services.TajweedRules;
using HolyQuran.Infrastructure.Services.SpecificNotes;
using HolyQuran.Infrastructure.Services.GeneralNotes;
using HolyQuran.Infrastructure.Services.Countries;
using HolyQuran.Infrastructure.Services.Chapters;

namespace HolyQuran.Infrastructure.Services.Interfaces
{
    public interface IInterfaceServices
    {       
        IFileService fileService { get; }    
        IUserService userService { get; }
        IStudentService studentService { get; }
        ITeacherService teacherService { get; }
        IRecordingService recordingService { get; }
        IEvaluationService evaluationService { get; }
        INoteService noteService { get; }
        ITajweedRuleService tajweedRuleService { get; }
        ISpecificNoteService specificNoteService { get; }
        IGeneralNoteService generalNoteService { get; }
        ICountriesService countriesService { get; }
        IChapterService chapterService { get; }

    }
}
