using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserMessage
    {
        public string UserName { get; set; }=string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsCurrentUser { get; set; }
        public DateTime DateSend { get; set; }
    }
}
