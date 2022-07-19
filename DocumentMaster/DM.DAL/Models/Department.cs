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
        [Required]
        public string Name { get; set; }=String.Empty;
        public ICollection<Worker> Workers { get; set; }        
        public ICollection<FileUnit> FileUnits { get; set; }
        public Department()
        {
            Workers = new List<Worker>();
            FileUnits = new List<FileUnit>();
            IsDeleted = false;
        }

    }
}
