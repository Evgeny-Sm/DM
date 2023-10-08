using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Release
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string ProjectCode { get; set; } = string.Empty;
        public ICollection<FileUnit>? FileUnits { get; set; }
        public Person? Creator { get; set; }
        public int PersonId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        public Project? Project { get; set; }
        public int ProjectId { get; set; }
        public bool IsLocked { get; set; }
        public bool IsRemoved { get; set; }
        public Release()
        {
            FileUnits = new List<FileUnit>();
        }

    }
}
