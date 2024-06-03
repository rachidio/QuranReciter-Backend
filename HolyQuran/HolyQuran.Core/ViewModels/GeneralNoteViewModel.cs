using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.ViewModels
{
    public class GeneralNoteViewModel : IBaseViewModel
    {
        public string GeneralNoteItem { get; set; }
        public int Note { get; set; }
        public string CreatedAt { get; set; }

    }
}
