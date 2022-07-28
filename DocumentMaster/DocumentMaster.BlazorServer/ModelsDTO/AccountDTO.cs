using System.Text.Json.Serialization;

namespace DocumentMaster.BlazorServer.ModelsDTO
{
    public class AccountDTO
    {
        [JsonPropertyName("Login")]
        public string Login { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
