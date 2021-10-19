using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Config
{
    public record History
    {
        [JsonPropertyName("created")]
        public string Created { get; set; }
        
        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("created_by")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        
        [JsonPropertyName("empty_layer")]
        public bool? EmptyLayer { get; set; }
    }
}
