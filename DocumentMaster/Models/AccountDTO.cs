using System.Text.Json.Serialization;

namespace Models
{
    public class AccountDTO
    {
        [JsonPropertyName("Login")]
        public string Login { get; set; }=String.Empty;
        [JsonPropertyName("Password")]
        public string Password { get; set; } = String.Empty;
    }
}
