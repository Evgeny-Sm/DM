using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class WorkerDTO
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
        [JsonPropertyName("WorkerActionDTOs")]
        public IEnumerable<WorkerActionDTO>? WorkerActionDTOs { get; set; }

        public WorkerDTO()
        {
            WorkerActionDTOs = new List<WorkerActionDTO>();
        }

    }
}
