using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FactorioMods.Models
{
    public class FactorioMods : Factorio
    {
        [JsonPropertyName("mods")]
        public IReadOnlyList<ModDesc> Mods { get; set; }
    }
}
