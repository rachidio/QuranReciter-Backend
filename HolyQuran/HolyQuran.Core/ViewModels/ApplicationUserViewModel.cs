using HolyQuran.Core.Constants;
using HolyQuran.Core.Enums;
using System.Text.Json.Serialization;

namespace HolyQuran.Core.ViewModels
{
    public class ApplicationUserViewModel : IBaseViewModel
    {
        public string Id { get; set; }
        public string? FullName { get; set; }
        public string DOB { get; set; }
        public string GenderType { get; set; }
        public string Country { get; set; }
        public string? Image { get; set; }
        public string? Phone { get; set; }
        public bool? IsDelete { get; set; }
        public string CreatedAt { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }     
        public string UserType { get; set; }
 
    }
}
