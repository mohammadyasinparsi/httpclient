using Rabani.Kernel.Utilites.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using HttpClient.Common;

namespace HttpClient.Interface
{
    public interface IHttpClientRequest
    {
        Task<ResultModel<TResult>> FormDataHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default);
        Task<ResultModel<TResult>> JsonHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default);
        Task<ResultModel<TResult>> UrlEncodedHttpClientRequest<TResult>(CreateHttpClientRequest createHttpClientRequest, CancellationToken cancellationToken = default);
    }
}
