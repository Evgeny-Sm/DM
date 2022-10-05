using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.DAL.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { get; set; }=String.Empty;
        [Required]
        [StringLength(500)]
        public string Password { get; set; }=String.Empty;
        [StringLength(20)]
        public string Role { get; set; } = String.Empty;
        public Person? Person { get; set; }       
        public int PersonId { get; set; }

    }
}
