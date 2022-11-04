using System.Net.Http;
using System.Net.Http.Json;

namespace TicTacDough.Client.Services {
    public class HttpRepository<T> : IDisposable, Contracts.IHttpRepository<T> where T : class {
        private readonly HttpClient httpClient;
        private readonly HttpInterceptorService interceptor;

        public HttpRepository(HttpClient client, HttpInterceptorService interceptor) {
            this.httpClient = client;
            this.interceptor = interceptor;
        }

        public async Task<List<T>> GetAll(string url)
        {
            this.interceptor.MonitorEvent();
            return await this.httpClient.GetFromJsonAsync<List<T>>($"{url}");
        }

        public void Dispose()
        {
            this.interceptor.DisposeEvent();
        }
    }
}