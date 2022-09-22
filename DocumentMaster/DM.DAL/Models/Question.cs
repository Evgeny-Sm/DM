using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Title { get; set; }=String.Empty;
        public DateTime DateTime { get; set; }
        public ICollection<Person> Persons { get; set; }=new List<Person>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public bool IsDeleted { get; set; }
        public ICollection<FileUnit> FileUnits { get; set; } =new List<FileUnit>();
        public int CreatorId { get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }

    }
}
