using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace TicTacDough.Client.Services {
    public class HttpInterceptorService {

        // members
        private readonly HttpClientInterceptor interceptor;
        private readonly NavigationManager navigationManager;

        // constructor
        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navigationManager) {
            this.interceptor = interceptor;
            this.navigationManager = navigationManager;
        }

        public void MonitorEvent() => this.interceptor.AfterSend += InterceptResponse;

        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var responseCode = e.Response.StatusCode;

                switch (responseCode)
                {
                    case HttpStatusCode.NotFound:
                        this.navigationManager.NavigateTo("/404");
                        message = "The requested resorce was not found.";
                        break;
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:
                        this.navigationManager.NavigateTo("/unauthorized");
                        message = "You are not authorized to access this resource. ";
                        break;
                    default:
                        this.navigationManager.NavigateTo("/500");
                        message = "Something went wrong, please contact Administrator";
                        break;
                }
                throw new HttpRequestException(message);
            }
        }

        public void DisposeEvent() => this.interceptor.AfterSend -= InterceptResponse;
    }
}