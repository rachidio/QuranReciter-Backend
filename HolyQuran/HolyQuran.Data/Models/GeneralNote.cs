using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class GeneralNote :BaseEntity
    {
        public string GeneralNoteItem { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
