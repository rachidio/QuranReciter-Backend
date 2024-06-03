using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Enums
{
    public enum LevelType
    {
        [Display(Name = "تأسيسي")]
        Foundation =1,
        [Display(Name = "تكميلي")]
        Supplementary = 2,
        [Display(Name = "متقدم")]
        Advanced =3,

    }
}
