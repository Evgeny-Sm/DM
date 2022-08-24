using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class UserAction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public FileUnit? FileUnit { get; set; }
        public int FileUnitId { get; set; }
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public int ActionNumber { get; set; }
        public bool IsConfirmed { get; set; }
        public double TimeForAction { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
