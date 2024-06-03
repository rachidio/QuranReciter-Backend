using HolyQuran.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.ViewModels
{
    public class TajweedRuleViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string NameArabic { get; set; }
        public string NameLatin { get; set; }
        public string CreatedAt { get; set; }

    }
}
