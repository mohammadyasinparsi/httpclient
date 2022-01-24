using HttpClient.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HttpClient.Builder
{
    public interface ICreateHttpRequestBuilder
    {
        void CreateBaseBuilder(Uri endPoint);
        void BuildTimeOut(int timeOut = 90);
        void BuildAuthenticate(string token);
        void BuildMethodType(RequestType requestType);
        void BuildHeader(Dictionary<string, string> customHeaders);
        void BuildContent(object Content, int sendRequstType);
        void BuildContentType(string contentTypes);
        HttpRequestMessage GetHttpRequestMessage();
    }
}
