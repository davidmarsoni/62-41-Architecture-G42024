using Azure;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace MVC.Services.QuerySnippet
{
    public static class QuerySnippet
    {
        #region HTTP Queries

        public static async Task<HttpResponseMessage?> GetOnURL(HttpClient httpClient, String url) {
            return EnsureSuccessOfQuery(await httpClient.GetAsync(url), "GET");
        }

        public static async Task<HttpResponseMessage?> PostOnUrl(HttpClient httpClient, String url, Object obj)
        {
            return EnsureSuccessOfQuery(await httpClient.PostAsJsonAsync(url, obj), "POST");
        }

        public static async Task<HttpResponseMessage?> PutOnUrl(HttpClient httpClient, String url, Object obj)
        {
            return EnsureSuccessOfQuery(await httpClient.PutAsJsonAsync(url, obj), "PUT");
        }

        public static async Task<HttpResponseMessage?> DeleteOnUrl(HttpClient httpClient, String url)
        {
            return EnsureSuccessOfQuery(await httpClient.DeleteAsync(url), "DELETE");
        }

        #endregion

        #region HTTP Response Handling

        public static HttpResponseMessage? EnsureSuccessOfQuery(HttpResponseMessage response, string OriginalOperation)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"{OriginalOperation} Request failed with message: {e.Message}");
                return null;
            }
        }

        public static T? HttpResponseHandling<T>(HttpResponseMessage? httpResponse)
        {
            if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                String responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                return JsonDeserialize<T>(responseBody);
            }
            else
            {
                return default;
            }
        }

        #endregion

        #region JSON Handling

        public static T? JsonDeserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOpt); ;
        }

        public static JsonSerializerOptions JsonSerializerOpt = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        #endregion
    }
}
