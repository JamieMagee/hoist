using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Docker.Registry.DotNet;
using Docker.Registry.DotNet.Authentication;
using Docker.Registry.DotNet.Registry;
using Hoist.Models.Registry;
using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;
using Hoist.Utilities;
using Microsoft.Extensions.Logging;
using Index = Hoist.Models.Registry.Index.Index;

namespace Hoist.Services
{
    public class DockerRegistryService : IDockerRegistryService
    {
        private readonly IRegistryClient _client;
        private readonly ILogger<IDockerRegistryService> _logger;

        public DockerRegistryService(ILogger<IDockerRegistryService> logger, string host, AuthenticationProvider authenticationProvider)
        {
            _logger = logger;
            _client = new RegistryClientConfiguration(host).CreateClient(authenticationProvider);
        }

        public async Task<IEnumerable<string>> ListRepositoriesAsync()
        {
            return (await _client.Catalog.GetCatalogAsync()).Repositories;
        }

        public async Task<IEnumerable<string>> ListRepositoryTagsAsync(string repositoryName)
        {
            return (await _client.Tags.ListImageTagsAsync(repositoryName)).Tags;
        }

        public async Task<ImageConfig> GetConfig(string repositoryName, string reference)
        {
            var response = await _client.Blobs.GetBlobAsync(repositoryName, reference);
            return await JsonSerializer.DeserializeAsync<ImageConfig>(response.Stream);
        }

        public async Task UploadManifest(string repositoryName, string reference, Manifest manifest)
        {
            var imageManifest = Converters.ConvertManifest(manifest);
            await _client.Manifest.PutManifestAsync(repositoryName, reference, imageManifest);
        }

        public async Task<(string digest, int size)> UploadConfigAsync(string repositoryName, ImageConfig config)
        {
            var content = JsonSerializer.Serialize(config, new JsonSerializerOptions
            {
                IgnoreNullValues = true
            });
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            var size = (int)stream.Length;
            var digest = Sha256Hash(content);
            await _client.BlobUploads.UploadBlobAsync(repositoryName, size, stream, digest);
            return (digest, size);
        }

        public async Task<Stream> DownloadLayer(string repositoryName, string reference)
        {
            var response = await _client.Blobs.GetBlobAsync(repositoryName, reference);
            var stream = new MemoryStream();
            await response.Stream.CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public async Task UploadLayer(string repositoryName, int contentLength, Stream stream, string digest)
        {
            try
            {
                await _client.BlobUploads.UploadBlobAsync(repositoryName, contentLength, stream, digest);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public async Task<Manifest> GetTagManifestAsync(string repositoryName, string reference, string architecture = "amd64",
            string os = "linux")
        {
            var response = await _client.Manifest.GetManifestAsync(repositoryName, reference);
            if (response.MediaType == MediaTypes.DockerManifest)
            {
                return JsonSerializer.Deserialize<Manifest>(response.Content);
            }

            var manifest = JsonSerializer.Deserialize<Index>(response.Content);
            response = await _client.Manifest.GetManifestAsync(
                repositoryName,
                manifest.Manifests.Single(m => m.Platform.Architecture == architecture && m.Platform.Os == os).Digest
            );
            return JsonSerializer.Deserialize<Manifest>(response.Content);
        }

        private static string Sha256Hash(string value)
        {
            var sb = new StringBuilder();

            using var hash = SHA256.Create();
            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(value));

            foreach (var b in result)
            {
                sb.Append(b.ToString("x2"));
            }

            return $"sha256:{sb}";
        }
    }
}
