using CrudPoc.Domain;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CrudPoc.Helper
{
    public class ApiClient
    {
        private readonly FileLog _fileLog;
        private readonly HttpClient _client;

        public ApiClient(string url)
        {
            _fileLog = new FileLog();
            _client = new HttpClient { BaseAddress = new Uri(url) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> Get<T>(string method)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(method);

            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }

            return default;
        }

        public async Task<bool> Post(string method, AnnouncementWebMotors anuncio)
        {
            return (await _client.PostAsync(method, new StringContent(JsonConvert.SerializeObject(anuncio), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
        }

        public async Task<bool> Put(string method, AnnouncementWebMotors anuncio)
        {
            return (await _client.PutAsync(method, new StringContent(JsonConvert.SerializeObject(anuncio), Encoding.UTF8, "application/json"))).IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string method)
        {
            return (await _client.DeleteAsync(method)).IsSuccessStatusCode;
        }
    }
}