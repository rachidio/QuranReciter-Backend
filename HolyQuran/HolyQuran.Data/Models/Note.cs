using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class Note :BaseEntity
    {
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }


        public List<GeneralNote> GeneralNote { get; set; }
        public List<SpecificNote> SpecificNote { get; set; }
    }
}
