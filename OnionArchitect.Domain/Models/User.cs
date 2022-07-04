﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitect.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int UserName { get; set; }
        public string Password { get; set; }
    }
}
