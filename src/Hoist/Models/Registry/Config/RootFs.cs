using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Config
{
    public record RootFs
    {
        [JsonPropertyName("diff_ids")]
        public List<string> DiffIds { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
