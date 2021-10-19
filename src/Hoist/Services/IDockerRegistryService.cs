using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;

namespace Hoist.Services
{
    public interface IDockerRegistryService
    {
        Task<IEnumerable<string>> ListRepositoryTagsAsync(string repositoryName);

        Task<IEnumerable<string>> ListRepositoriesAsync();

        Task<Manifest> GetTagManifestAsync(string repositoryName, string reference, string architecture = "amd64", string os = "linux");

        Task<ImageConfig> GetConfig(string repositoryName, string reference);

        Task<Stream> DownloadLayer(string repositoryName, string reference);

        Task UploadLayer(string repositoryName, int contentLength, Stream stream, string digest);

        Task UploadManifest(string repositoryName, string reference, Manifest manifest);

        Task<(string digest, int size)> UploadConfigAsync(string repositoryName, ImageConfig config);
    }
}
