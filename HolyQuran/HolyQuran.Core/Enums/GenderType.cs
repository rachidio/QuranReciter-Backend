using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace HolyQuran.Core.Enums
{
    public enum GenderType
    {
        [Display(Name = "ذكر")]
        [EnumMember(Value = "ذكر")]

        Male = 1,
        [Display(Name = "أنثى")]
        [EnumMember(Value = "أنثى")]

        Female = 2
    }
}
