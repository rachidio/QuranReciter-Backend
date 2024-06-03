using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.ViewModels
{
    public class CountryViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string LanguageEn { get; set; }
        public string LanguageAr { get; set; }
    }
}
