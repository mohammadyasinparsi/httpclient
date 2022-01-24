using Rabani.Kernel.Utilites.ValueCheck;
using HttpClient.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using HttpClient.Helpers;
using Newtonsoft.Json;
using System.IO;

namespace HttpClient.Builder
{
    public class ConcereteHttpRequestBuilder : ICreateHttpRequestBuilder
    {
        private HttpRequestMessage _httpRequestMessage;

        public ConcereteHttpRequestBuilder()
        {
            Reset();
        }
        public void Reset()
        {
            _httpRequestMessage = new HttpRequestMessage();
        }
        public void CreateBaseBuilder(Uri endPoint)
        {
            _httpRequestMessage.RequestUri = endPoint;
        }

        public void BuildHeader(Dictionary<string, string> customHeaders)
        {
            if (customHeaders != null && customHeaders.Count > 0)
            {
                foreach (var item in customHeaders.Where(w => w.Key.Trim().ToLower() != "contenttype"))
                {
                    _httpRequestMessage.Headers.Add(item.Key, item.Value);
                }
            }
        }
        public void BuildContent(object Content, int sendRequstType)
        {
            switch (sendRequstType)
            {
                case 1:
                    _httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(Content));
                    break;
                case 2:
                    var requestContent = new MultipartFormDataContent();
                    byte[] binaryFile = (byte[])Content;
                    requestContent.Add(new StreamContent(new MemoryStream(binaryFile)));
                    _httpRequestMessage.Content = requestContent;
                    break;
                case 3:
                    if (Content != null)
                    {
                        var keyValues = Extension.GetProperties(Content);
                        _httpRequestMessage.Content = new FormUrlEncodedContent(keyValues);
                    }
                    break;
            }
        }
        public void BuildContentType(string contentTypes)
        {

            if (contentTypes.ContainsString())
                _httpRequestMessage.Content.Headers.ContentType.MediaType = contentTypes;
        }
        public void BuildMethodType(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Get:
                    _httpRequestMessage.Method = HttpMethod.Get;
                    break;
                case RequestType.Post:
                    _httpRequestMessage.Method = HttpMethod.Post;
                    break;
                case RequestType.Put:
                    _httpRequestMessage.Method = HttpMethod.Put;
                    break;
                case RequestType.Delete:
                    _httpRequestMessage.Method = HttpMethod.Delete;
                    break;
                case RequestType.Option:
                    _httpRequestMessage.Method = HttpMethod.Options;
                    break;
            }
        }

        public HttpRequestMessage GetHttpRequestMessage()
        {
            HttpRequestMessage result = this._httpRequestMessage;
            this.Reset();
            return result;
        }

        public void BuildTimeOut(int timeOut = 90)
        {
            _httpRequestMessage.SetTimeout(TimeSpan.FromSeconds(timeOut));
        }

        public void BuildAuthenticate(string token)
        {
            if (token.ContainsString())
                _httpRequestMessage.Headers.Add(HttpRequestHeader.Authorization.ToString(), token);
        }


    }
}
