using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MVC.Services.QuerySnippet
{
    public static class QuerySnippet
    {
        public static JsonSerializerOptions JsonSerializerOpt = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<HttpResponseMessage?> QueryOnURL(HttpClient httpClient, String url) {
            var response = await httpClient.GetAsync(url);
            try { 
                response.EnsureSuccessStatusCode();
            } catch (HttpRequestException e)
            {
                Console.WriteLine($"Request failed with message: {e.Message}");
                return null;
            }
            return response;
        }
    }
}
