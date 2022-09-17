using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public int CreatorId { get; set; }

        [StringLength(1000)]
        public string Content { get; set; }=string.Empty;
        public DateTime DateTime { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public ICollection<Person> Persons { get; set; } = new List<Person>();
        public bool HasFile { get; set; }
        [StringLength(200)]
        public string Path { get; set; } = string.Empty;

    }
}
