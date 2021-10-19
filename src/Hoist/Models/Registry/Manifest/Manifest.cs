using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Manifest
{
    public record Manifest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("schemaVersion")]
        public int SchemaVersion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("mediaType")]
        public string MediaType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("config")]
        public Descriptor.Descriptor Config { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("layers")]
        public IList<Descriptor.Descriptor> Layers { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("annotations")]
        public IDictionary<string, string> Annotations { get; set; }
    }
}
