using PlushkinForms.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlushkinForms.Services
{
    class BookmarkService
    {
        const string Url = "http://188.226.96.115:8000/core/bookmarks/";
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            if (Application.Current.Properties.ContainsKey("authToken"))
            {
                string authToksen = Application.Current.Properties["authToken"].ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", authToksen);
            }
            
            return client;
        }

        public async Task<IEnumerable<Bookmark>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonSerializer.Deserialize<IEnumerable<Bookmark>>(result, options);
        }

        public async Task<Bookmark> Add(Bookmark bookmark)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(bookmark),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Bookmark>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Bookmark> Update(Bookmark bookmark)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(Url,
                new StringContent(
                    JsonSerializer.Serialize(bookmark),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Bookmark>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<Bookmark> Delete(int id)
        {
            HttpClient client = GetClient();
            var response = await client.DeleteAsync(Url + id);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<Bookmark>(
               await response.Content.ReadAsStringAsync(), options);
        }
    }
}
