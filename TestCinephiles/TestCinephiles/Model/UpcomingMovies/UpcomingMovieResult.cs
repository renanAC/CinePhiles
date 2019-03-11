using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestCinephiles.Model.UpcomingMovies
{
    public class UpcomingMovieResult
    {

        public UpcomingMovieResult()
        {
            Results = new List<UpcomingMovie>();
        }

        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
        [JsonProperty("dates")]
        public Date Dates { get; set; }
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        public List<UpcomingMovie> Results { get; set; }
    }

    public class Date
    {
        [JsonProperty("maximum")]
        public string Maximun { get; set; }
        [JsonProperty("minimum")]
        public string Manimun { get; set; }
    }
}
