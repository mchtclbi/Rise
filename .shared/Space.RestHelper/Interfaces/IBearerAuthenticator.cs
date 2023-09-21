namespace Space.RestHelper.Interfaces
{
    public interface IBearerAuthenticator : IAuthenticator
    {
        public string Authenticate(string token);
    }
}