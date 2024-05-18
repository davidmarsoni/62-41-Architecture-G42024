using System.Net;
using QS = MVC.Services.QuerySnippet.QuerySnippet;
namespace MVC.Services.QuerySnippet
{
    public class StandardQuerySet
    {
        public static async Task<IEnumerable<T>?> GetAll<T>(HttpClient httpClient, String url)
        {
            HttpResponseMessage? httpResponse = await QS.GetOnURL(httpClient, url);
            if (httpResponse != null && httpResponse.StatusCode == HttpStatusCode.OK)
            {
                String responseBody = await httpResponse.Content.ReadAsStringAsync();
                return QS.JsonDeserialize<IEnumerable<T>>(responseBody);
            }
            else
            {
                return null;
            }
        }

        public static async Task<T?> Get<T>(HttpClient httpClient, String url)
        {
            return QS.HttpResponseHandling<T>(await QS.GetOnURL(httpClient, url));
        }


    }
}
