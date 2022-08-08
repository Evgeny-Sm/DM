using System.Text.Json.Serialization;

namespace DocumentMaster.BlazorServer.ModelsDTO
{
    public class Person1DTO
    {

        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Login { get; set; }=String.Empty;
        [JsonPropertyName("role")]
        public string Role { get; set; } = String.Empty;
        [JsonPropertyName("access_token")]
        public string ShortToken { get; set; } = String.Empty;

    }
}
