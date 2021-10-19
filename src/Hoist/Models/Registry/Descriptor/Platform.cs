using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Descriptor
{
    public record Platform
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("architecture")]
        public string Architecture { get; init; }
        
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("os")]
        public string Os { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("os.version")]
        public string OsVersion { get; init; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonPropertyName("os.features")]
        public string OsFeatures { get; init; }
        
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("variant")]
        public string Variant { get; init; }
    }
}
