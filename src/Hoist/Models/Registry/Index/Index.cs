using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Index
{
    public record Index
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("manifests")]
        public IEnumerable<Descriptor.Descriptor> Manifests { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("annotations")]
        public IDictionary<string, string> Annotations { get; init; }
    }
}
