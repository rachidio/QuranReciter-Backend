using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.ViewModels
{
    public class ChapterViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public string Arabic { get; set; }
        public string Latin { get; set; }
        public string English { get; set; }
        public string Localtion { get; set; }
        public string Sajda { get; set; }
        public int Ayah { get; set; }
    }
}
