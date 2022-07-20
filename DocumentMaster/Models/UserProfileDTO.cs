using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class UserProfileDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; } = String.Empty;
        [JsonPropertyName("LastName")]
        public string LastName { get; set; } = String.Empty;
        [JsonPropertyName("PersonId")]
        public int PersonId { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }= false;
        [JsonPropertyName("DepartmentId")]
        public int DepartmentId { get; set; }
        [JsonPropertyName("UserActionDTOs")]
        public IEnumerable<UserActionDTO>? UserActionDTOs { get; set; }

        public UserProfileDTO()
        {
            UserActionDTOs = new List<UserActionDTO>();
        }

    }
}
