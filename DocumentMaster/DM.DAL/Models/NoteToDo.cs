using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class NoteToDo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public Note? Note { get; set; }
        public int NoteId { get; set; }
        public bool IsDoing { get; set; }
    }
}
