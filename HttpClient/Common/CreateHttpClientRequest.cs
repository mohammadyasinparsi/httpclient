using System;
using System.Collections.Generic;

namespace HttpClient.Common
{
    public class CreateHttpClientRequest
    {
        public RequestType RequestType { get; set; }
        public Uri EndPoint { get; set; }
        public string AuthorizationToken { get; set; }
        public object Data { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string ContentTypes { get; set; }
        public int SecondTimeOut { get; set; }
    }
}
