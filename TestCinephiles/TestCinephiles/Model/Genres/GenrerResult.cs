using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestCinephiles.Model.Genres
{
    public class GenrerResult
    {
        [JsonProperty("genres")]
        public List<Genrer> Genres { get; set; }
    }
}
