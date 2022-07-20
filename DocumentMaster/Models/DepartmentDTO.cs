using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class DepartmentDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }=false;
        [JsonPropertyName("Name")]
        public string Name { get; set; } = String.Empty;
        [JsonPropertyName("UserProfileDTOs")]
        public IEnumerable<PersonDTO> PersonDTOs { get; set; }
        [JsonPropertyName("FileDTOs")]
        public IEnumerable<FileDTO> FileDTOs { get; set; }
        public DepartmentDTO()
        { 
            PersonDTOs = new List<PersonDTO>();
            FileDTOs = new List<FileDTO>();
        }

    }
}
