using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class UpdateSpecificNoteDto
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime TimeSpecificNote { get; set; }
        [Required]
        public string SpecificNoteItem { get; set; }
        [Required]
        public int NoteId { get; set; }
        [Required]
        public int TajweedRuleId { get; set; }

    }
}
