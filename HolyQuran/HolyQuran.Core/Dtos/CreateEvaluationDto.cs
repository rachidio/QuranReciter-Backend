
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class CreateEvaluationDto
    {
        [Required]
        [DefaultValue("1")]
        public int TeacherId { get; set; }
        [Required]
        [DefaultValue("1")]
        public int RecordingId { get; set; }

    }
}
