using RestSharp;
using System.Text.Json;
using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Concretes
{
    public class JsonContent : IContent
    {
        public RestRequest GetRestRequest(object data)
        {
            var restRequest = new RestRequest();
            restRequest.AddJsonBody(JsonSerializer.Serialize(data), "application/json");

            return restRequest;
        }
    }
}