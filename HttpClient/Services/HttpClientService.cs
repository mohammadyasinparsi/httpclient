using Newtonsoft.Json;
using Rabani.Kernel.Utilites.Data.Common;
using Rabani.Kernel.Utilites.ValueCheck;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HttpClient.Builder;
using HttpClient.Common;
using HttpClient.Helpers;
using HttpClient.Interface;


namespace HttpClient.Services
{
    public class HttpClientService : IHttpClientRequest
    {
        private readonly ICreateHttpRequestBuilder _builder;


        public HttpClientService(ICreateHttpRequestBuilder builder)
        {
            _builder = builder;
        }
        public async Task<ResultModel<TResult>> JsonHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default)
        {
            TimeoutHandler handler = CreateHandler(createHttpClientRequest.SecondTimeOut);

            if (!createHttpClientRequest.EndPoint.AbsoluteUri.ContainsString())
                throw new Exception("اطلاعاتی دریافت نشد");

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler))
            {
                client.Timeout = Timeout.InfiniteTimeSpan;

                var request = CreateRequest(createHttpClientRequest,1);
                HttpResponseMessage response = await client.SendAsync(request, cancellationToken);
                return ModifyResponse<TResult>(response);
            }
        }

        public async Task<ResultModel<TResult>> FormDataHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default)
        {
            TimeoutHandler handler = CreateHandler(createHttpClientRequest.SecondTimeOut);
            if (!createHttpClientRequest.EndPoint.AbsoluteUri.ContainsString())
                throw new Exception("اطلاعاتی دریافت نشد");

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler))
            {
                var request = CreateRequest(createHttpClientRequest,2);

                HttpResponseMessage response = await client.SendAsync(request, cancellationToken);
                return ModifyResponse<TResult>(response);
            }

        }

        public async Task<ResultModel<TResult>> UrlEncodedHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default)
        {
            TimeoutHandler handler = CreateHandler(createHttpClientRequest.SecondTimeOut);

            if (!createHttpClientRequest.EndPoint.AbsoluteUri.ContainsString())
                throw new Exception("اطلاعاتی دریافت نشد");

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(handler))
            {
                var request = CreateRequest(createHttpClientRequest,3);
                HttpResponseMessage response = await client.SendAsync(request, cancellationToken);
                return ModifyResponse<TResult>(response);
            }

        }

        private HttpRequestMessage CreateRequest(CreateHttpClientRequest createHttpClientRequest,int requestType)
        {
            IHttpRequestDirector director = new HttpRequestDirector(_builder);
            director.BuildFullHttpRequest(createHttpClientRequest, requestType);
            return _builder.GetHttpRequestMessage();
        }

        private ResultModel<TResult> ModifyResponse<TResult>(HttpResponseMessage httpResponseMessage)
        {

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return ResultModel<TResult>.Failed((int)httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, ErrorTypes.BadRequest, new Dictionary<string, string>
                {
                    {"Content",httpResponseMessage.Content?.ReadAsStringAsync().Result}
                });
            }
            else
            {

                return ResultModel<TResult>.Succeed((int)httpResponseMessage.StatusCode, JsonConvert.DeserializeObject<TResult>(httpResponseMessage.Content.ReadAsStringAsync().Result), "Succed");
            }

        }

        private TimeoutHandler CreateHandler(int seconds)
        {
            return new TimeoutHandler
            {
                DefaultTimeout = TimeSpan.FromSeconds(seconds),
                InnerHandler = new HttpClientHandler()
            };
        }

    }
}
