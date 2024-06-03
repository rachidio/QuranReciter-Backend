using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Enums
{
    public enum UserType
    {
        [Display(Name = "مدير نظام")]
        Admin = 1,
        [Display(Name = "معلم")]
        Teacher = 2,
        [Display(Name = "طالب")]
        Student = 3,

    }
}
