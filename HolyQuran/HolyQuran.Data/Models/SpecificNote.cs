using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class SpecificNote :BaseEntity
    {
        public DateTime TimeSpecificNote { get; set; }
        public string SpecificNoteItem { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }

        public int? TajweedRuleId { get; set; }
        public TajweedRule TajweedRule { get; set; }
    }
}
