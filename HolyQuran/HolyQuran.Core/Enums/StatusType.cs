using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HolyQuran.Core.Enums
{
    public enum StatusType
    {
        [Display(Name = "انتظار")]
        Pending = 1,
        [Display(Name = "تم")]
        Approved = 2,
        [Display(Name = "رفض")]
        Rejected = 3

    }
}
