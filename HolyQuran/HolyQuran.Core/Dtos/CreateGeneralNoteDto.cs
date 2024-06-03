using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class CreateGeneralNoteDto
    {
        [Required]
        [DefaultValue("ملاحظة عامة")]
        public string GeneralNoteItem { get; set; }
        [Required]
        [DefaultValue("1")]
        public int NoteId { get; set; }
     }
}
