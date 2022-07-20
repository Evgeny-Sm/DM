using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }=String.Empty;
        public string LastName { get; set; }=String.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Person? Person { get; set; }
        public int PersonId { get; set; }

    }
}
