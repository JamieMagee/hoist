using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;

namespace Hoist.Services
{
    public interface IDockerRebaseService
    {
        bool CanRebase(string baseNameLabel, string baseDigestLabel);

        Task<bool> IsNewBaseImageAvailableAsync();

        IAsyncEnumerable<(Stream layer, int size, string digest)> DownloadNewLayersAsync(Manifest manifest, ImageConfig config);
        
        Task<ImageConfig> GenerateNewImageConfig();

        Task<Manifest> GenerateNewImageManifest(string digest, int size);
    }
}
