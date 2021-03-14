using System.Text.Json.Serialization;

namespace FactorioMods.Models
{
    public class ModDesc
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
