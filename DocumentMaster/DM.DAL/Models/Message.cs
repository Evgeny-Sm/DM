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
        [StringLength(2000)]
        public string Message { get; set; } = String.Empty;
        [StringLength(2000)]
        public string Description { get; set; } = String.Empty;
        [StringLength(50)]
        public string UserName { get; set; }=string.Empty;
        public DateTime DateSend { get; set; }

    }
}
