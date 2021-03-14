using System.Text.Json.Serialization;

namespace FactorioMods.Models
{
    public class InfoDesc
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("contact")]
        public string Contact { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }
    }
}
