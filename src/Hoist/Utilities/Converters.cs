using System.Linq;
using Docker.Registry.DotNet.Models;
using Manifest = Hoist.Models.Registry.Manifest.Manifest;

namespace Hoist.Utilities
{
    public static class Converters
    {
        public static ImageManifest2_2 ConvertManifest(Manifest manifest)
        {

            var config = new Config
            {
                Digest = manifest.Config.Digest,
                Size = manifest.Config.Size,
                Urls = manifest.Config.Urls?.ToArray(),
                MediaType = manifest.Config.MediaType
            };

            var manifestLayers = manifest.Layers.Select(layer => new ManifestLayer
                {
                    Digest = layer.Digest,
                    Size = layer.Size,
                    Urls = layer.Urls?.ToArray(),
                    MediaType = layer.MediaType
                })
                .ToList();

            var imageManifest = new ImageManifest2_2
            {
                SchemaVersion = manifest.SchemaVersion,
                MediaType = manifest.MediaType,
                Config = config,
                Layers = manifestLayers.ToArray()
            };

            return imageManifest;
        }
    }
}
