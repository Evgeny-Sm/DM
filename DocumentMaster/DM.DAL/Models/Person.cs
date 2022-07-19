﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    [Index(nameof(Login), IsUnique = true)]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? Login { get; set; }
        [Required]
        [StringLength(100)]
        public string? Password { get; set; }
        [StringLength(30)]
        public string? Role { get; set; }
        public Worker? Worker { get; set; }
    }
}
