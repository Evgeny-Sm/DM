using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class NotifyCountDTO
    {
        public int UnreadedCount { get; set; }
        public int ToCheckCount { get; set; }
        public int ToDoCount { get; set; }

    }
}
