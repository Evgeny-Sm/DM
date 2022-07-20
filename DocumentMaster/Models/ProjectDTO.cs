using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class ProjectDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; } = false;
        [JsonPropertyName("Name")]
        public string Name { get; set; } = String.Empty;
        [JsonPropertyName("Description")]
        public string Description { get; set; }= String.Empty;
        [JsonPropertyName("Client")]
        public string Client { get; set; } = String.Empty;
        [JsonPropertyName("UserProfilerId")]
        public int UserProfilerId { get; set; }
        [JsonPropertyName("FileDTOs")]
        public IEnumerable<FileDTO> FileDTOs { get; set; }
        public ProjectDTO()
        { 
            FileDTOs = new List<FileDTO>();
        }

    }
}
