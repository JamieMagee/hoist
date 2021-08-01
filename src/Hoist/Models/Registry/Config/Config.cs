using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Config
{
    public class Config
    {
        [JsonPropertyName("User")]
        public string User { get; init; }

        [JsonPropertyName("ExposedPorts")]
        public Dictionary<string, object> ExposedPorts { get; init; }

        [JsonPropertyName("Env")]
        public List<string> Env { get; init; }

        [JsonPropertyName("Entrypoint")]
        public List<string> Entrypoint { get; init; }

        [JsonPropertyName("Cmd")]
        public List<string> Cmd { get; init; }

        [JsonPropertyName("Volumes")]
        public Dictionary<string, object> Volumes { get; init; }

        [JsonPropertyName("WorkingDir")]
        public string WorkingDir { get; init; }

        [JsonPropertyName("Labels")]
        public Dictionary<string, string> Labels { get; init; }

        [JsonPropertyName("StopSignal")]
        public string StopSignal { get; init; }
    }
}
