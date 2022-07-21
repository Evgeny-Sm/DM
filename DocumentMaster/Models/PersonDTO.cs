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
        [JsonPropertyName("Login")]
        public string Login { get; set; }= String.Empty;
        [JsonPropertyName("Password")]
        public string Password { get; set; }=String.Empty;
        [JsonPropertyName("Role")]
        public string Role { get; set; }=String.Empty ;
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; } = String.Empty;
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = String.Empty;
        [JsonPropertyName("DepartmentId")]
        public int DepartmentId { get; set; }
        [JsonPropertyName("PositionId")]
        public int PositionId { get; set; }
        [JsonPropertyName("AccessLevel")]
        public int AccessLevel { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; } = false;

    }
}
