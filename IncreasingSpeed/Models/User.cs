using System.Text.Json.Serialization;

namespace IncreasingSpeed.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("username")]
        public string UserName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
