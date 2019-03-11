namespace TestCinephiles.Model.UpcomingMovies
{
    public class UpcomingMovieRequest
    {
        public string language { get; set; }
        public int page { get; set; }
        public string region { get; set; }
        public string api_key { get; set; }
        public string query { get; set; }
    }
}
