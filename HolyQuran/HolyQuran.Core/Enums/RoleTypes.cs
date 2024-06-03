using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Enums
{
    public enum RoleTypes
    {
        [Display(Name = "مدير نظام")]
        Admin = 0,
        [Display(Name = "معلم")]
        Teacher = 1,
        [Display(Name = "طالب")]
        Student = 2,
    }
}
