
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class UpdateRecordingDto
    {
        public int Id { get; set; }
        [Required]
        public double Duration { get; set; }
        [Required]
        public IFormFile File_path { get; set; }
        [Required]
        public int ChapterId { get; set; }
        [Required]
        public int FromChapter { get; set; }
        [Required]
        public int ToChapter { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public RecordingStatus RecordingStatus { get; set; }
    }
}
