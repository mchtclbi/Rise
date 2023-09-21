using System;
using RestSharp;
using System.Linq;
using System.Collections.Generic;
using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Concretes
{
    public class FormUrlDecodedContent : IContent
    {
        public RestRequest GetRestRequest(object data)
        {
            if (data is Dictionary<string, string>)
            {
                var restRequest = new RestRequest();
                restRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                ((Dictionary<string, object>)data).ToList().ForEach(q => restRequest.AddParameter(q.Key, q.Value, ParameterType.GetOrPost));

                return restRequest;
            }

            throw new Exception($"{data.GetType()} is not supported");
        }
    }
}