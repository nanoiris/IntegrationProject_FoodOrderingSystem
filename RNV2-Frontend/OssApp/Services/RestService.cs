﻿using OssApp.Model;
using Serilog;
using System.Net.Http.Json;

namespace OssApp.Services
{
    public class RestService<T>
    {
        internal HttpClient http;
        public string server;
        
        public RestService(string server)
        {
            this.server = server;
            http = new HttpClient();
            http.BaseAddress = new Uri(server);
            if(AuthService.User != null)
              http.DefaultRequestHeaders.Add("Authorization", $"Bearer {AuthService.User.Token}");
        }

        public async Task<List<T>> List(string listUrl)
        {
            var response = await http.GetAsync(listUrl);
            List<T> rows = null;
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("RestService.List");
                try
                {
                    rows = response.Content.ReadFromJsonAsync<List<T>>()
                        .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.List : {ex.Message}");
                }
            }
            if (rows == null)
            {
                rows = new List<T>();
            }
            return rows;
        }

        public async Task<List<T>> List(string listUrl,HttpContent content)
        {
            http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            var response = await http.PostAsync(listUrl,content);
            List<T> rows = null;
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("RestService.List");
                try
                {
                    //Log.Debug(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    rows = await response.Content.ReadFromJsonAsync<List<T>>();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.List : {ex.Message}");
                }
            }
            if (rows == null)
            {
                rows = new List<T>();
            }
            return rows;
        }

        public async Task<T> FindOne(string url)
        {
            var response = await http.GetAsync(url);
            T row = default(T);
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("RestService.Find");
                try
                {
                    row = response.Content.ReadFromJsonAsync<T>()
                        .GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Log.Debug($"RestService.Find : {ex.Message}");
                }
            }
            return row;
        }

        public bool DeleteOne(string url)
        {
            Log.Debug($"RestService.DeleteOne {url}");
            var response = http.DeleteAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            Log.Debug($"response {response}");
            return false;
        }

        public string UpdateOne(string url, MultipartFormDataContent content)
        {
            var response = http.PutAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadFromJsonAsync<AppResult>().GetAwaiter().GetResult();
                if (result.isSuccess)
                {
                    return result.message;
                }
            }
            return null;
        }
        public string AddNewOne(string url, MultipartFormDataContent content)
        {
            Log.Debug($"Enter RestService : AddNewOne : {url}");
            var response = http.PostAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Add new one succefully");
                var result = response.Content.ReadFromJsonAsync<AppResult>().GetAwaiter().GetResult();
                if (result.isSuccess)
                {
                    return result.message;
                }
            }
            return null;
        }

        public string AddNewOne(string url, HttpContent content)
        {
            Log.Debug($"Enter RestService : AddNewOne : {url}");
            var response = http.PostAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Add new one succefully");
                var result = response.Content.ReadFromJsonAsync<AppResult>().GetAwaiter().GetResult();
                if (result.isSuccess)
                {
                    return result.message;
                }
            }
            return null;
        }
        public bool AddNewOneAction(string url, MultipartFormDataContent content)
        {
            Log.Debug($"Enter RestService : AddNewOne : {url}");
            var response = http.PostAsync(url, content).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                Log.Debug("Add new one succefully");
                var result = response.Content.ReadFromJsonAsync<AppResult>().GetAwaiter().GetResult();
                return result.isSuccess;
            }
            return false;
        }
    }
}
