using HttpClient.Common;

namespace HttpClient.Builder
{

    public class HttpRequestDirector : IHttpRequestDirector
    {
        private ICreateHttpRequestBuilder _createHttpRequestBuilder;

        public HttpRequestDirector(ICreateHttpRequestBuilder builder)
        {
            _createHttpRequestBuilder = builder;

        }

        public void BuildFullHttpRequest(CreateHttpClientRequest createHttpClientRequest,int requestType)
        {
            this._createHttpRequestBuilder.CreateBaseBuilder(createHttpClientRequest.EndPoint);
            this._createHttpRequestBuilder.BuildMethodType(createHttpClientRequest.RequestType);
            this._createHttpRequestBuilder.BuildTimeOut(createHttpClientRequest.SecondTimeOut);
            this._createHttpRequestBuilder.BuildAuthenticate(createHttpClientRequest.AuthorizationToken);
            this._createHttpRequestBuilder.BuildHeader(createHttpClientRequest.Headers);
            this._createHttpRequestBuilder.BuildContent(createHttpClientRequest.Data,requestType);
            this._createHttpRequestBuilder.BuildContentType(createHttpClientRequest.ContentTypes);
        }
    }
}
