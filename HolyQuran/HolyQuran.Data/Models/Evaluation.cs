using HolyQuran.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class Evaluation :BaseEntity
    {
        //public StatusType StatusType { get; set; }
        public SumbitStatus SumbitStatus { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int RecordingId { get; set; }
        public Recording Recording { get; set; }
        public Evaluation()
        {
            //StatusType = StatusType.Pending;
            SumbitStatus = SumbitStatus.Save;
        }
    }
}
