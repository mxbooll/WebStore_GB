using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebStore_GB.Clients.Base
{
    public abstract class BaseClient
    {
        protected readonly HttpClient _client;
        protected readonly string _serviceAddress;

        protected BaseClient(IConfiguration configuration, string serviceAddress)
        {
            _serviceAddress = serviceAddress;

            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiUri"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }
    }
}
