using Newtonsoft.Json;

namespace Core.Requests
{
    public class FacebookAuthRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("userID")]
        public string UserId { get; set; }

        [JsonProperty("expiresIn")]
        public long ExpiresIn { get; set; }

        [JsonProperty("signedRequest")]
        public string SignedRequest { get; set; }

        [JsonProperty("graphDomain")]
        public string GraphDomain { get; set; }

        [JsonProperty("data_access_expiration_time")]
        public long DataAccessExpirationTime { get; set; }
    }
}
