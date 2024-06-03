using HolyQuran.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.ViewModels
{
    public class EvaluationViewModel
    {
        public int Id { get; set; }
        //public string StatusType { get; set; }
        public string SumbitStatus { get; set; }
        public string Teacher { get; set; }
        public RecordingViewModel Recording { get; set; }

        public string CreatedAt { get; set; }

    }
}
