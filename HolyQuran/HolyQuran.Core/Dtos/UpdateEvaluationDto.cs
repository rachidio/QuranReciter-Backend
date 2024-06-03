
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class UpdateEvaluationDto
    {
        public int Id { get; set; }
        //[Required]
        //public StatusType StatusType { get; set; }
        [Required]
        public SumbitStatus SumbitStatus { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int RecordingId { get; set; }
    }
}
