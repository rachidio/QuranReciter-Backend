using HolyQuran.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.ViewModels
{
    public class SpecificNoteViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string TimeSpecificNote { get; set; }
        public string SpecificNoteItem { get; set; }
        public int Note { get; set; }
        public string CreatedAt { get; set; }
        public TajweedRuleViewModel TajweedRule { get; set; }

    }
}
