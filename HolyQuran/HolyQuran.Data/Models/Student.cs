﻿using HolyQuran.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Data.Models
{
    public class Student : BaseEntity
    {

        public LevelType LevelType { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }     
    }
}