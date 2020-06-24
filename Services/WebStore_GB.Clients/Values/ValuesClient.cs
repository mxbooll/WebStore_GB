using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using WebStore_GB.Clients.Base;
using WebStore_GB.Interfaces.TestApi;

namespace WebStore_GB.Clients.Values
{
    public class ValuesClient : BaseClient, IValueService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration, "api/values")
        {
        }

        public HttpStatusCode Delete(int id)
        {
            var responce = _client.DeleteAsync($"{_serviceAddress}/{id}").Result;
            return responce.StatusCode;
        }

        public IEnumerable<string> Get()
        {
            var responce = _client.GetAsync(_serviceAddress).Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadAsAsync<IEnumerable<string>>().Result;
            }
            
            return Enumerable.Empty<string>();
        }

        public string Get(int id)
        {
            var responce = _client.GetAsync($"{_serviceAddress}/{id}").Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadAsAsync<string>().Result;
            }

            return string.Empty;
        }

        public Uri Post(string value)
        {
            var responce = _client.PostAsJsonAsync(_serviceAddress, value).Result;
            return responce.EnsureSuccessStatusCode().Headers.Location;
        }

        public HttpStatusCode Update(int id, string value)
        {
            var responce = _client.PutAsJsonAsync($"{_serviceAddress}/{id}", value).Result;
            return responce.EnsureSuccessStatusCode().StatusCode;
        }
    }
}
