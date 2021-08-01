using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hoist.Models.Registry.Config
{
    /// <summary>
    /// Image is the JSON structure which describes some basic information about the image.
    /// This provides the <code>application/vnd.docker.container.image.v1+json</code> mediatype when marshalled to JSON
    /// </summary>
    public class ImageConfig
    {
        /// <summary>
        /// Created is the combined date and time at which the image was created, formatted as defined by RFC 3339, section 5.6.
        /// </summary>
        [JsonPropertyName("created")]
        public string Created { get; set; }

        /// <summary>
        /// Author defines the name and/or email address of the person or entity which created and is responsible for maintaining the image.
        /// </summary>
        [JsonPropertyName("author")]
        public string Author { get; set; }

        /// <summary>
        /// Architecture is the CPU architecture which the binaries in this image are built to run on.
        /// </summary>
        [JsonPropertyName("architecture")]
        public string Architecture { get; set; }

        /// <summary>
        /// OS is the name of the operating system which the image is built to run on.
        /// </summary>
        [JsonPropertyName("os")]
        public string Os { get; set; }
        
        /// <summary>
        /// Config defines the execution parameters which should be used as a base when running a container using the image.
        /// </summary>
        [JsonPropertyName("config")]
        public Config Config { get; set; }

        /// <summary>
        /// RootFS references the layer content addresses used by the image.
        /// </summary>
        [JsonPropertyName("rootfs")]
        public RootFs RootFs { get; set; }

        /// <summary>
        /// History describes the history of each layer.
        /// </summary>
        [JsonPropertyName("history")]
        public List<History> History { get; set; }
    }
}
