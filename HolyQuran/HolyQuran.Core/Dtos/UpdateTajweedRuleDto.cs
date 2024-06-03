using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class UpdateTajweedRuleDto
    {
        public int Id { get; set; }
        [Required]
        public string NameArabic { get; set; }
        [Required]
        public string NameLatin { get; set; }
    }
}
