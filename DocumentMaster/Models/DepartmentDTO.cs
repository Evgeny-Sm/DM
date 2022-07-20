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
        [JsonPropertyName("WorkerDTOs")]
        public IEnumerable<WorkerDTO> WorkerDTOs { get; set; }
        [JsonPropertyName("FileDTOs")]
        public IEnumerable<FileDTO> FileDTOs { get; set; }
        public DepartmentDTO()
        { 
            WorkerDTOs = new List<WorkerDTO>();
            FileDTOs = new List<FileDTO>();
        }

    }
}
