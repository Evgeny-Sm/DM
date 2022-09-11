using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public string Description { get; set; }=String.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsDeleted { get; set; }=false;
        public ICollection<Person> Persons { get; set; }=new List<Person>();


    }
}
