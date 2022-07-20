using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class FileDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; } = false;   
        [JsonPropertyName("Name")]
        public string Name { get; set; } = String.Empty;
        [JsonPropertyName("Description")]
        public string Description { get; set; }=string.Empty;
        [JsonPropertyName("PathFile")]
        public string PathFile { get; set; } = String.Empty;
        [JsonPropertyName("DepartmentId")]
        public int DepartmentId { get; set; }
        [JsonPropertyName("ProjectId")]
        public int ProjectId { get; set; }
      

    }
}
