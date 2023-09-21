using System;
using System.Text;
using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Concretes
{
    public class BasicAuthenticator : IBasicAuthenticator
    {
        public string Authenticate(string username, string password) =>
            $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))}";
    }
}