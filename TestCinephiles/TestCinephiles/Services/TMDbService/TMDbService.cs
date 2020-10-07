using System.Threading.Tasks;
using TestCinephiles.Helper;
using TestCinephiles.Model.Genres;
using TestCinephiles.Model.UpcomingMovies;
using TestCinephiles.Services.RequestProvider;

namespace TestCinephiles.Services.TMDbService
{
    class TMDbService : ITMDbService
    {
        private const string UpcomingURL = "/movie/upcoming";
        private const string GenresURL = "/genre/movie/list";
        private const string UpcomingSearchURL = "/search/movie";
        private readonly IRequestProvider _requestProvider;

        public TMDbService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<UpcomingMovieResult> GetUpcomingMovies(UpcomingMovieRequest upcomingMovieRequest)
        {
            try
            {
                var uri = UriHelper.CombineUri(Settings.TMDbBaseEndpoint, UpcomingURL);
                string queryString = _requestProvider.ConvertQueryString(upcomingMovieRequest);
                return await _requestProvider.GetAsync<UpcomingMovieResult>(uri + queryString);
            }
            catch (System.Exception ex)
            {
                return new UpcomingMovieResult();
            }

        }


        public async Task<GenrerResult> GetGenrer()
        {
            try
            {
                var uri = UriHelper.CombineUri(Settings.TMDbBaseEndpoint, $"{GenresURL}?api_key={Settings.TMDbApiKey}");
                return await _requestProvider.GetAsync<GenrerResult>(uri);
            }
            catch (System.Exception ex)
            {
                return new GenrerResult();
            }

        }

        public async Task<UpcomingMovieResult> GetUpcomingMoviesSearchAsync(UpcomingMovieRequest upcomingMovieRequest)
        {
            try
            {
                var uri = UriHelper.CombineUri(Settings.TMDbBaseEndpoint, UpcomingSearchURL);
                string queryString = _requestProvider.ConvertQueryString(upcomingMovieRequest);
                return await _requestProvider.GetAsync<UpcomingMovieResult>(uri + queryString);
            }
            catch (System.Exception ex)
            {
                return new UpcomingMovieResult();
            }
        }
    }
}
