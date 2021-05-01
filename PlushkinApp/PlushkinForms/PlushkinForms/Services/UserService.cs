﻿using PlushkinForms.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlushkinForms.Services
{
    class UserService
    {
        const string Url = "http://188.226.96.115:8000/core/";
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        //public async Task<IEnumerable<Bookmark>> Get()
        //{
        //    HttpClient client = GetClient();
        //    string result = await client.GetStringAsync(Url);
        //    return JsonSerializer.Deserialize<IEnumerable<Bookmark>>(result, options);
        //}

        public async Task<UserErrorMessage> Registration(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync("http://188.226.96.115:8000/core/user_registration/",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.Created)
                return null;


            return JsonSerializer.Deserialize<UserErrorMessage>(
                await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<AuthToken> GetAuthToken(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync("http://188.226.96.115:8000/core/auth_token/",
                new StringContent(
                    JsonSerializer.Serialize(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonSerializer.Deserialize<AuthToken>(
                await response.Content.ReadAsStringAsync(), options);
        }

        //public async Task<Bookmark> Update(Bookmark bookmark)
        //{
        //    HttpClient client = GetClient();
        //    var response = await client.PutAsync(Url,
        //        new StringContent(
        //            JsonSerializer.Serialize(bookmark),
        //            Encoding.UTF8, "application/json"));

        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return null;

        //    return JsonSerializer.Deserialize<Bookmark>(
        //        await response.Content.ReadAsStringAsync(), options);
        //}

        //public async Task<Bookmark> Delete(int id)
        //{
        //    HttpClient client = GetClient();
        //    var response = await client.DeleteAsync(Url + id);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //        return null;

        //    return JsonSerializer.Deserialize<Bookmark>(
        //       await response.Content.ReadAsStringAsync(), options);
        //}
    }
}