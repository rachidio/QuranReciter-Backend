using HolyQuran.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace HolyQuran.Core.ViewModels
{
    public class NoteViewModel : IBaseViewModel
    {
        public int Id { get; set; }
        public int Evaluation { get; set; }
        public string CreatedAt { get; set; }

    }
}
