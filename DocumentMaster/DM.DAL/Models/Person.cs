using Microsoft.EntityFrameworkCore;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Account Account { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Position? Position { get; set; }
        public int PositionId { get; set; }
        public string TelegramContact { get; set; }
        public double SalaryPerH { get; set; }
        public ICollection<UserAction> UserActions { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Person()
        { 
            UserActions= new List<UserAction>();
            Account= new Account();
        }
    }
}
