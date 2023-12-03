using System.Text.Json.Serialization;

namespace API.DTO
{
    public class UserInfoDto
    {
        [JsonPropertyName("first-name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last-name")]
        public string LastName { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}
