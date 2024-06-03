using HolyQuran.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.ViewModels
{
    public class RecordingViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public double Duration { get; set; }
        public string File_path { get; set; }
        public string Chapter { get; set; }
        public int FromChapter { get; set; }
        public int ToChapter { get; set; }
        public string Student { get; set; }
        public string CreatedAt { get; set; }
        public string RecordingStatus { get; set; }
    }
}
