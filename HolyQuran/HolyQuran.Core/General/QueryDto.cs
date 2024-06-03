
using HolyQuran.Core.Enums;
using System.ComponentModel;

namespace HolyQuran.Core.General
{
    public class  QueryDto
    {
        [DefaultValue(1)]
        public int Page { get; set; }
        public string GeneralSearch { get; set; }
        public SumbitStatus? SumbitStatus { get; set; }
        public RecordingStatus? RecordingStatus { get; set; }

    }
}
