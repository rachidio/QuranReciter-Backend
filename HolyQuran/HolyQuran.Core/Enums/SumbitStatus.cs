using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HolyQuran.Core.Enums
{
    public enum SumbitStatus
    {
        [Display(Name = "ارسال")]
        Send = 1,
        [Display(Name = "حفظ")]
        Save = 2
    }
}
