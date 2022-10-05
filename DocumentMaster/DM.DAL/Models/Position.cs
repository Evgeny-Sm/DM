using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Position
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }=String.Empty;
        public ICollection<Person> Persons { get; set; }
        public Position()
        { 
            Persons = new List<Person>();
        }
    }
}
