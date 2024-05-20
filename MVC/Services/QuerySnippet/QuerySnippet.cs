using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Build.Construction;
using System.Net;
using System.Text.Json;

namespace MVC.Services.QuerySnippet
{
    public static class QuerySnippet
    {
        public static String GETALL = "GETALL";
        public static String GET = "GET";
        public static String POST = "POST";
        public static String PUT = "PUT";
        public static String DELETE = "DELETE";

        #region HTTP Queries

        public static async Task<HttpResponseMessage?> GetOnURL(HttpClient httpClient, String url) {
            return await httpClient.GetAsync(url);
        }

        public static async Task<HttpResponseMessage?> PostOnUrl(HttpClient httpClient, String url, Object obj)
        {
            return await httpClient.PostAsJsonAsync(url, obj);
        }

        public static async Task<HttpResponseMessage?> PutOnUrl(HttpClient httpClient, String url, Object obj)
        {
            return await httpClient.PutAsJsonAsync(url, obj);
        }

        public static async Task<HttpResponseMessage?> DeleteOnUrl(HttpClient httpClient, String url)
        {
            return await httpClient.DeleteAsync(url);
        }

        #endregion

        #region HTTP Response Handling

        public static T? HttpResponseHandling<T>(HttpResponseMessage? httpResponse, string originalOperation)
        {
            if (isHttpResponseMessageSuccess(httpResponse, originalOperation)) {
                String responseBody = httpResponse.Content.ReadAsStringAsync().Result;
                return JsonDeserialize<T>(responseBody);
            }
            return default;
        }

        public static Boolean isHttpResponseMessageSuccess(HttpResponseMessage? httpResponse, string originalOperation) {
            try
            {
                httpResponse.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{originalOperation} Request failed with message: {e.Message}");
                return false;
            }
        }

        #endregion

        #region JSON Handling

        public static T? JsonDeserialize<T>(string json)
        {
            try {
                return JsonSerializer.Deserialize<T>(json, JsonSerializerOpt); ;
            } catch (Exception e)
            {
                Console.WriteLine($"Failed to deserialize JSON: {e.Message}");
                return default;
            }
            
        }

        public static JsonSerializerOptions JsonSerializerOpt = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        #endregion
    }
}
