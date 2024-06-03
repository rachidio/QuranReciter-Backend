using HolyQuran.Core.Constants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using HolyQuran.Core.Enums;

namespace HolyQuran.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public DateTime DOB { get; set; }
        public GenderType GenderType { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public int? Code { get; set; }
        public UserType UserType { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public List<Teacher> Teacher { get; set; }
        public List<Student> Student { get; set; }
        public List<EmailLog> EmailLog { get; set; }
        public List<Logging> Logging { get; set; }
        public List<Notification> Notification { get; set; }

        public ApplicationUser()
        {
            IsDelete = false;
            CreatedAt = DateTime.Now;
            GenderType = GenderType.Male;
            UserType = UserType.Admin;
         }
    }
}
