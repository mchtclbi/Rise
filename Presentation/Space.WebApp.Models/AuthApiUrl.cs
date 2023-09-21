namespace Space.WebApp.Models
{
    public  class AuthApiUrl
    {
        public string BaseUrl { get; set; }
        public AuthApiEndpoint Endpoint { get; set; }
    }

    public class AuthApiEndpoint
    {
        public string Login { get; set; }
    }
}