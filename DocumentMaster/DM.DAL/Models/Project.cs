using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } 
        public string Name { get; set; }=String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Client { get; set; }=String.Empty;
        public Person? MainIng { get; set; }
        public int PersonId { get; set; }
        public ICollection<FileUnit>? FileUnits { get; set; }
        public Project()
        { 
            FileUnits = new List<FileUnit>();
        }
    }
}
