using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ControlDTO
    {
        public int Id { get; set; }
        public int FileUnitId { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
        public bool IsConfirmed { get; set; }
        public bool IsInAction { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
        public double TimeForChecking { get; set; }
    }
}
