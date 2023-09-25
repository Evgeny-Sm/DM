using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class QuestionsToDo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public Person? Person { get; set; }
        public int PersonId { get; set; }
        public Question? Question { get; set; }
        public int QuestionId { get; set; }
        public bool IsDoing { get; set; }
    }
}
