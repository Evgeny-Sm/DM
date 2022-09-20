using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    public class SendedMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string UserName { get; set; }=string.Empty;
        public DateTime DateSend { get; set; }

    }
}
