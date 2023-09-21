﻿using RestSharp;
using System.Collections.Generic;
using Space.RestHelper.Interfaces;

namespace Space.RestHelper.Models
{
    public class RestRequestModel
    {
        public string? Endpoint { get; set; }
        public object Data { get; set; }
        public Method Method { get; set; }
        public IContent Content { get; set; }
        public string? Authenticator { get; set; }
        public Dictionary<string, string> Header { get; set; } = null;
    }
}