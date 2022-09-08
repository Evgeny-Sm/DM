using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class Control
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public FileUnit FileUnit { get; set; }
        public int FileUnitId { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsInAction { get; set; }
        public DateTime DateTime { get; set; }
        public double TimeForChecking { get; set; }



    }
}
