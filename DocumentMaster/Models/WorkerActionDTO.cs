using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class WorkerActionDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("FileUnitId")]
        public int FileUnitId { get; set; }
        [JsonPropertyName("WorkerId")]
        public int WorkerId { get; set; }
        [JsonPropertyName("ActionNumber")]
        public int ActionNumber { get; set; }
        [JsonPropertyName("CreatedDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
