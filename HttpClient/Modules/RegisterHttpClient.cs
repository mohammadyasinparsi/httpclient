using HttpClient.Builder;
using HttpClient.Interface;
using HttpClient.Services;
using Microsoft.Extensions.DependencyInjection;


namespace HttpClient.Modules
{
    public static class RegisterHttpClient
    {
        /// <summary>
        /// Add IHttpClientRequest As Scoped
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabaniHttpClientService(this IServiceCollection services)
        {
            services.AddScoped<ICreateHttpRequestBuilder, ConcereteHttpRequestBuilder>();
            services.AddScoped<IHttpClientRequest, HttpClientService>();

            return services;
        }
    }
}
