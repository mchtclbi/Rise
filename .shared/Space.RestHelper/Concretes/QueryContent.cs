using System;
using RestSharp;
using System.Linq;
using System.Collections.Generic;
using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Concretes
{
    public class QueryContent : IContent
    {
        public RestRequest GetRestRequest(object data)
        {
            var restRequest = new RestRequest();

            if (data is null) return restRequest;

            if (data is Dictionary<string, object> dictionary)
            {
                dictionary.ToList().ForEach(q => restRequest.AddParameter(q.Key, q.Value, ParameterType.QueryString));
                return restRequest;
            }

            throw new Exception($"{data.GetType()} is not supported");
        }
    }
}