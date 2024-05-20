using System.Net;
using QS = MVC.Services.QuerySnippet.QuerySnippet;
namespace MVC.Services.QuerySnippet
{
    public class StandardQuerySet
    {
        public static async Task<IEnumerable<T>?> GetAll<T>(HttpClient httpClient, String url)
        {
            return QS.HttpResponseHandling<IEnumerable<T>>(await QS.GetOnURL(httpClient, url), QS.GETALL);
        }

        public static async Task<T?> Get<T>(HttpClient httpClient, String url)
        {
            return QS.HttpResponseHandling<T>(await QS.GetOnURL(httpClient, url), QS.GET);
        }

        public static async Task<T?> Post<T>(HttpClient httpClient, String url, T obj)
        {
            return QS.HttpResponseHandling<T>(await QS.PostOnUrl(httpClient, url, obj), QS.POST);
        }

        public static async Task<Boolean> PutNoReturn<T>(HttpClient httpClient, String url, T obj)
        {
            HttpResponseMessage? httpResponseMessage = await QS.PutOnUrl(httpClient, url, obj);
            if (QS.isHttpResponseMessageSuccess(httpResponseMessage, QS.PUT))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public static async Task<Boolean> Delete(HttpClient httpClient, String url)
        {
            HttpResponseMessage? httpResponseMessage = await QS.DeleteOnUrl(httpClient, url);
            if (QS.isHttpResponseMessageSuccess(httpResponseMessage, QS.DELETE))
            {
                return true;
            } else {

                return false;
            }
        }
    }
}
