using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.Enums
{
    public enum ActiveType
    {
        [Display(Name = "نعم")]
        True = 0,
        [Display(Name = "لا")]
        False = 1,

    }
}
