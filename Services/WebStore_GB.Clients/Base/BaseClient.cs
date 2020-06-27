﻿using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        public T Get<T>(string url) => GetAsync<T>(url).Result;

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }

        public HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item)
        {
            var response = await _client.PostAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        public HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item)
        {
            var response = await _client.PutAsJsonAsync(url, item);
            return response.EnsureSuccessStatusCode();
        }

        public HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;

        public async Task<HttpResponseMessage> DeleteAsync(string url) => await _client.DeleteAsync(url);
    }
}
