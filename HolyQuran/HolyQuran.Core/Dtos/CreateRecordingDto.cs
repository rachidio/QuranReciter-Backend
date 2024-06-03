
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class CreateRecordingDto
    {
        [Required]
        [DefaultValue("01.12")]
        public double Duration { get; set; }
        [Required]
        public IFormFile File_path { get; set; }
        [Required]
        [DefaultValue("1")]
        public int ChapterId { get; set; }
        [Required]
        [DefaultValue("2")]
        public int FromChapter { get; set; }
        [Required]
        [DefaultValue("5")]
        public int ToChapter { get; set; }
        [Required]
        [DefaultValue("1")]
        public int StudentId { get; set; }
   

    }
}
