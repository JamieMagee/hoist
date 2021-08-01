using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Descriptor
{
    /// <summary>
    /// Descriptor describes the disposition of targeted content.
    /// This structure provides <code>application/vnd.oci.descriptor.v1+json</code> mediatype
    /// when marshalled to JSON.
    /// </summary>
    public class Descriptor
    {
        /// <summary>
        /// MediaType is the media type of the object this schema refers to.
        /// </summary>
        [JsonPropertyName("mediaType")]
        public string MediaType { get; init; }

        /// <summary>
        /// Digest is the digest of the targeted content.
        /// </summary>
        [JsonPropertyName("digest")]
        public string Digest { get; set; }

        // Size specifies the size in bytes of the blob.
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        /// URLs specifies a list of URLs from which this object MAY be downloaded
        /// </summary>
        [JsonPropertyName("urls")]
        public IEnumerable<string> URLs { get; set; }

        // Annotations contains arbitrary metadata relating to the targeted content.
        [JsonPropertyName("annotations")]
        public IDictionary<string, string> Annotations { get; set; }

        /// <summary>
        /// Platform describes the platform which the image in the manifest runs on.
        /// This should only be used when referring to a manifest
        /// </summary>
        [JsonPropertyName("platform")]
        public Platform Platform { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var d = (Descriptor)obj;
            return Digest == d.Digest;
        }
    }
}
