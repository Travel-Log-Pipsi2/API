namespace Core.Requests
{
    public class FacebookAuthRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string AccessToken { get; set; }

        public string UserId { get; set; }

        public long ExpiresIn { get; set; }

        public string SignedRequest { get; set; }

        public string GraphDomain { get; set; }

        public long DataAccessExpirationTime { get; set; }
    }
}
