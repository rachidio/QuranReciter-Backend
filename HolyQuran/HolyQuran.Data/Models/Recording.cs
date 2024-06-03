using HolyQuran.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class Recording :BaseEntity
    {
        public double Duration { get; set; }
        public string File_path { get; set; }
        public int FromChapter { get; set; }
        public int ToChapter { get; set; }
        public RecordingStatus RecordingStatus { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
        public Recording()
        {
            RecordingStatus = RecordingStatus.NotEvaluateYet;
        }
    }
}
