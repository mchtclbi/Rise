using RestSharp;

namespace Space.RestHelper.Interfaces
{
    public interface IContent
    {
        public RestRequest GetRestRequest(object data);
    }
}