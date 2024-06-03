using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HolyQuran.Core.Enums
{
    public enum RecordingStatus
    {
        [Display(Name = "قيد التقييم")]
        NotEvaluateYet = 1,
        [Display(Name = "لم يتم التقييم")]
        PendingEvaluation = 2,
        [Display(Name = "تم التقييم")]
        DoneEvaluate = 3

    }
}
