using HolyQuran.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class CreateSpecificNoteDto
    {
        [Required]
        [DataType(DataType.Time)]
        [DefaultValue("01:22")]
        public DateTime TimeSpecificNote { get; set; }
        [Required]
        [DefaultValue("ملاحظة بتوقيت معين")]
        public string SpecificNoteItem { get; set; }
        [Required]
        [DefaultValue("1")]
        public int NoteId { get; set; }
        [Required]
        [DefaultValue("1")]
        public int TajweedRuleId { get; set; }
    }
}
