using System.Threading.Tasks;
using TestCinephiles.Model.Genres;
using TestCinephiles.Model.UpcomingMovies;

namespace TestCinephiles.Services.TMDbService
{
    public interface ITMDbService
    {
       Task<UpcomingMovieResult> GetUpcomingMovies(UpcomingMovieRequest upcomingMovieRequest);
       Task<UpcomingMovieResult> GetUpcomingMoviesSearchAsync(UpcomingMovieRequest upcomingMovieRequest);
       Task<GenrerResult> GetGenrer();
    }
}
