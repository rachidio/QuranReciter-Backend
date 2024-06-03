
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class CreateTajweedRuleDto
    {
        [Required]
        [DefaultValue("الإقلاب")]

        public string NameArabic { get; set; }
        [Required]
        [DefaultValue("الإقلاب")]
        public string NameLatin { get; set; }
    }
}
