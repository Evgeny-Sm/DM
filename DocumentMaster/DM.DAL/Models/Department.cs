using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }=String.Empty;
        public string Description { get; set; }=String.Empty;
        public ICollection<Person> Persons { get; set; }        
        public ICollection<FileUnit> FileUnits { get; set; }
        public Department()
        {
            Persons = new List<Person>();
            FileUnits = new List<FileUnit>();
            IsDeleted = false;
        }

    }
}
