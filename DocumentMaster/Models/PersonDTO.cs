using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class PersonDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; } = String.Empty;
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = String.Empty;
        [JsonPropertyName("DepartmentId")]
        public int DepartmentId { get; set; }
        [JsonPropertyName("PositionId")]
        public int PositionId { get; set; }

        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [JsonPropertyName("TelegramContact")]
        public string TelegramContact { get; set; }
        [JsonPropertyName("SalaryPerH")]
        public double SalaryPerH { get; set; }



    }
}
