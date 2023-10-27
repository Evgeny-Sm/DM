using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ReleaseDTO
    {
        public int Id { get; set; }
        public string ProjectCode { get; set; } = string.Empty;
        public List<int> FilesIds { get; set; } = new();
        public int PersonId { get; set; }
        public string PersonName { get; set; } = string.Empty;
        public int MainIngId { get; set; }
        public string CreateDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public bool IsLocked { get; set; }
        public bool IsRemoved { get; set; }
        public string ProjectName { get; set; } = string.Empty;
    }
}
