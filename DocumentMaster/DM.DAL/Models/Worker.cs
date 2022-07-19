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
        public string FirstName { get; set; }
        public int LastName { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<FileUnit> FileUnits { get; set; }
        public Worker()
        {
            FileUnits = new List<FileUnit>();
            IsDeleted = false;
        }


    }
}
