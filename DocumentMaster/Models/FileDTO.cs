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
        public bool IsDeleted { get; set; }    
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
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public double TimeToCreate { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public int NumbersDrawings { get; set; }
        public string Status { get; set; } = string.Empty;
        [JsonPropertyName("PersonId")]
        public int PersonId { get; set; }
        public DateTime CreateDate { get; set; }


    }
}
