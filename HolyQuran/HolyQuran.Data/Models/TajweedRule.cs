using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class TajweedRule :BaseEntity
    {
        public string NameArabic { get; set; }
        public string NameLatin { get; set; }

        public List<SpecificNote> SpecificNote { get; set; }


    }
}
