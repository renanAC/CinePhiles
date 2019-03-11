using Newtonsoft.Json;

namespace TestCinephiles.Model.Genres
{
    public class Genrer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
