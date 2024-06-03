﻿namespace HolyQuran.Data.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
 
        public BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDelete = false;
         }
    }
}
