using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ChallengeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<int> PersonIds { get; set; } = new();
    }
}
