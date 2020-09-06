using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using eBus.Model;
using Newtonsoft.Json;

namespace eBus.web.Helper
{
    public class APIService
    {
        public static string Username { get; set; } = null;
        public static string Password { get; set; } = null;

        private readonly string _route;
        public APIService(string route)
        {
            _route = route;
        }

        public async Task<HttpResponseMessage> Get(object search)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                       $"{Username}:{Password}")));

            string url = "";
            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            //http://localhost:64472/api

            //http://localhost:62248 od ovog
            try
            {
                client.BaseAddress = new Uri($"http://localhost:62248");
                return await client.GetAsync($"api/{_route}{url}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<HttpResponseMessage> GetById(object id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                       $"{Username}:{Password}")));

            client.BaseAddress = new Uri($"http://localhost:62248");
            return await client.GetAsync($"api/{_route}/{id}");
        }

        public async Task<HttpResponseMessage> Insert(object request)
        {
            var client = new HttpClient(); //1

            client.DefaultRequestHeaders.Authorization = //2. autorizacija
            new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                       $"{Username}:{Password}")));


            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json"); // 3. request pretvaramo


            var url = $"http://localhost:62248/api/{_route}"; // 4. 
            return await client.PostAsync(url, data);
        }

        public async Task<HttpResponseMessage> Update(object id, object request)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                       $"{Username}:{Password}")));

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"http://localhost:62248/api/{_route}/{id}";
            return await client.PutAsync(url, data);
        }
    }
}
