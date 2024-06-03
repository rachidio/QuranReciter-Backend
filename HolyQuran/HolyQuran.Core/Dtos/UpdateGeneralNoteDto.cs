using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class UpdateGeneralNoteDto
    {
        public int Id { get; set; }
        [Required]
        public string GeneralNoteItem { get; set; }
        [Required]
        public int NoteId { get; set; }
    }
}
