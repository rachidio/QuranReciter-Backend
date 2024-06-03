using HolyQuran.Core.Constants;
using HolyQuran.Core.Enums;
using HolyQuran.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataType = System.ComponentModel.DataAnnotations.DataType;
namespace HolyQuran.Core.Dtos
{


    public class CreateApplicationUserDto
    {
        [Required]
        [StringLength(Message.MaxLength100, ErrorMessage = Message.ErrorMessage.Max100_Min3Length, MinimumLength = Message.MinLength3)]
        [DefaultValue("أحمد العامودي")]

        public string FullName { get; set; }
        [Required(ErrorMessage = Message.ErrorMessage.RightPhoneEnter)]
        [RegularExpression(Message.RegularExpPhone, ErrorMessage = Message.ErrorMessage.RightPhoneEnter)]
        [DefaultValue("0599855555")]

        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [DefaultValue("quran@gmail.com")]

        //[RegularExpression(Message.RegularExpEmail)]
        public string Email { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }
        [Required]
        public GenderType GenderType { get; set; }
        [Required]
        [DefaultValue(1)]
        public int CountryId { get; set; }
        public UserType UserType { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = Message.ErrorMessage.Max100_Min6Length, MinimumLength = Message.MinLength6)]
        [DataType(DataType.Password)]
        [DefaultValue("Ahmed11$$")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = Message.DescriptionConfirmPassword)]
        [Compare(Message.Password, ErrorMessage = Message.ErrorMessage.PassAndConfirmPassNotSame)]
        [DefaultValue("Ahmed11$$")]
        public string ConfirmPassword { get; set; }

    }
}
