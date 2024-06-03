using System;

namespace HolyQuran.Core.Dtos
{
    public class Query
    {
        public string GeneralSearch { get; set; }
         public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
