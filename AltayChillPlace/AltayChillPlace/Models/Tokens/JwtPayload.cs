using Newtonsoft.Json;

namespace AltayChillPlace.Models.Tokens
{
    public class JwtPayload
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("iat")]
        public long IssuedAt { get; set; }

        [JsonProperty("exp")]
        public long ExpiresAt { get; set; }
    }
}
