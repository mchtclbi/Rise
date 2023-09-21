using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Concretes
{
    public class BearerAuthenticator : IBearerAuthenticator
    {
        public string Authenticate(string token) => $"Bearer {token}";
    }
}