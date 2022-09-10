﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }=String.Empty;
        public string LastName { get; set; }= String.Empty;
        public Account Account { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Position? Position { get; set; }
        public int PositionId { get; set; }
        public string TelegramContact { get; set; } = String.Empty;
        public double SalaryPerH { get; set; }
        public ICollection<Challenge> Challenges { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsConfirmed { get; set; }=false;
        public Person()
        { 
            Challenges= new List<Challenge>();
            Account= new Account();
        }
    }
}
