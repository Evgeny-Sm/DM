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
        public string Name { get; set; }=String.Empty;
        [AllowNull]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string PathFile { get; set; } = String.Empty;
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public Worker? WorkerCreator { get; set; }
        public int WorkerCreatorId { get; set; }
        public Worker? Inspector { get; set; }
        public int InspectorId{ get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }


    }
}
