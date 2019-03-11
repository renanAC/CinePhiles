using System.Threading.Tasks;

namespace TestCinephiles.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
        string ConvertQueryString(object queryStringObject);
    }
}
