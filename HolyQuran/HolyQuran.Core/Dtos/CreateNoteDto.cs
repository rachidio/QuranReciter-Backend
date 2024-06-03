
using HolyQuran.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Dtos
{
    public class CreateNoteDto
    {
        [Required]
        [DefaultValue("1")]
        public int EvaluationId { get; set; }
    }
}
