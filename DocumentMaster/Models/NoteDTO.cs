using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public List<int> PersonIds { get; set; } = new();
        public int QuestionId { get; set; }
        public bool HasFile { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
