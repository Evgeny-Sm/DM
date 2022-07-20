using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string FirstName { get; set; }=String.Empty;
        public string LastName { get; set; }=String.Empty;
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }=false;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<WorkerAction> WorkerActions { get; set; }
        public Worker()
        {
            WorkerActions = new List<WorkerAction>();
        }


    }
}
