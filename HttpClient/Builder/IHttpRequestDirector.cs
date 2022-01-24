using HttpClient.Common;

namespace HttpClient.Builder
{
    public interface IHttpRequestDirector
    {
        void BuildFullHttpRequest(CreateHttpClientRequest createHttpClientRequest,int requestType);
    }
}