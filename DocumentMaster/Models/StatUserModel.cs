using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StatUserModel
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public PersonDTO? Person { get; set; }
        public ProjectDTO? Project { get; set; }
        public List<FileDTO> DevelopedFiles { get; set; } = new List<FileDTO>();
        public List<ControlDTO> Controls { get; set; } = new List<ControlDTO>();

    }
}
