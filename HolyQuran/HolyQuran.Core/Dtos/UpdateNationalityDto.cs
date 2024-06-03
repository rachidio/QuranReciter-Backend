using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Core.Dtos
{
    public class UpdateNationalityDto
    {

        public int Id { get; set; }    
        public string? Name{ get; set; }
        
    }
}
