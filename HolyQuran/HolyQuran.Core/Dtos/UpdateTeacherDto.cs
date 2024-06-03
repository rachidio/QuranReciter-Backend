using HolyQuran.Core.Constants;
using HolyQuran.Core.Enums;
using HolyQuran.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace HolyQuran.Core.Dtos
{
    public class UpdateTeacherDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [Required]
        [StringLength(Message.MaxLength100, ErrorMessage = Message.ErrorMessage.Max100_Min3Length, MinimumLength = Message.MinLength3)]
        public string? FullName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }
        [Required]
        public GenderType GenderType { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        [RegularExpression(Message.RegularExpPhone, ErrorMessage = Message.ErrorMessage.RightPhoneEnter)]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        //[RegularExpression(Message.RegularExpEmail)]
        public string Email { get; set; }
        public IFormFile Image { get; set; }

    }
}
