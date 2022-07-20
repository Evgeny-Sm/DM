using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class WorkerAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public FileUnit? FileUnit { get; set; }
        public int FileUnitId { get; set; }
        public Worker? Worker { get; set; }
        public int WorkerId { get; set; }
        public int ActionNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
