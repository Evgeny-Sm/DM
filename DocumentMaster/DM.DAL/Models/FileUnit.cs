using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class FileUnit
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }=String.Empty;
        [StringLength(500)]
        public string Description { get; set; }=string.Empty;
        [StringLength(200)]
        public string PathFile { get; set; } = String.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }
        public Section? Section { get; set; }
        public int SectionId { get; set; }
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public ICollection<Control>? Controls { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public int NumbersDrawings { get; set; }
        public double TimeToCreate { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;
        public string Status { get; set; }
        public bool IsOldVersion { get; set; }
        [StringLength(200)]
        public string ProjectCode { get; set; } = string.Empty;
        public FileUnit()
        { 
            Controls = new List<Control>();
        }

    }
}
