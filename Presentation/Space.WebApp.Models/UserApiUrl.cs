namespace Space.WebApp.Models
{
    public class UserApiUrl
    {
        public string BaseUrl { get; set; }
        public UserApiEndpoint Endpoint { get; set; }
    }

    public class UserApiEndpoint
    {
        public string Register { get; set; }
    }
}