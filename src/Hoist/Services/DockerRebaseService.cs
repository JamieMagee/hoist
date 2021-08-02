using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hoist.Extensions;
using Hoist.Models;
using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;
using Microsoft.Extensions.Logging;

namespace Hoist.Services
{
    public class DockerRebaseService : IDockerRebaseService
    {
        private readonly IDockerRegistryServiceFactory _factory;
        private readonly ImageConfig _imageConfig;
        private readonly ILogger<DockerRebaseService> _logger;
        private readonly Manifest _manifest;

        private DockerImageReference _imageReference;
        private IDockerRegistryService _registryService;

        public DockerRebaseService(IDockerRegistryServiceFactory factory, Manifest manifest, ImageConfig imageConfig,
            ILogger<DockerRebaseService> logger)
        {
            _factory = factory;
            _manifest = manifest;
            _imageConfig = imageConfig;
            _logger = logger;
        }

        public bool CanRebase(string baseNameLabel, string baseDigestLabel)
        {
            var canRebase =  _imageConfig.Config.Labels != null &&
                             _imageConfig.Config.Labels.ContainsKey(baseNameLabel) &&
                             _imageConfig.Config.Labels.ContainsKey(baseDigestLabel);
            
            if (canRebase)
            {
                _imageReference = ExtractBaseImageReference(_imageConfig);
                _registryService = _factory.GetDockerRegistryService(_imageReference.Host);
            }

            return canRebase;
        }

        public async Task<bool> IsNewBaseImageAvailableAsync()
        {
            var availableBaseImageTags = await _registryService.ListRepositoryTagsAsync(_imageReference.Repository);
            return availableBaseImageTags.Contains(_imageReference.Tag);
        }

        public async Task<ImageConfig> GenerateNewImageConfig()
        {
            var newImageConfig = _imageConfig.Clone();
            var currentBaseImageConfig = await GetBaseImageConfigAsync();
            var newBaseImageConfig = await GetNewBaseImageConfigAsync();
            
            // Replace Diff IDs
            newImageConfig.RootFs.DiffIds =
                newBaseImageConfig.RootFs.DiffIds.Concat(_imageConfig.RootFs.DiffIds.Where(diffId =>
                    !currentBaseImageConfig.RootFs.DiffIds.Contains(diffId))).ToList();
            
            // Replace History
            newImageConfig.History = newBaseImageConfig.History.Concat(_imageConfig.History.Where(history =>
                !currentBaseImageConfig.History.Contains(history))).ToList();
            
            // TODO: Replace labels

            return newImageConfig;
        }
        
        public async Task<Manifest> GenerateNewImageManifest(string digest, int size)
        {
            var newImageManifest = _manifest.Clone();
            var currentBaseImageManifest = await GetBaseImageManifestAsync();
            var newBaseImageManifest = await GetNewBaseImageManifestAsync();
            
            // Refer to new config
            newImageManifest.Config.Digest = digest;
            newImageManifest.Config.Size = size;

            // Replace Layers
            newImageManifest.Layers =
                newBaseImageManifest.Layers.Concat(_manifest.Layers.Where(layer => !currentBaseImageManifest.Layers.Contains(layer)))
                    .ToList();

            return newImageManifest;
        }

        public async IAsyncEnumerable<(Stream layer, int size, string digest)> DownloadNewLayersAsync(Manifest manifest, ImageConfig config)
        {
            var newBaseImageManifest = await _registryService.GetTagManifestAsync(_imageReference.Repository, _imageReference.Tag);
            foreach (var layer in newBaseImageManifest.Layers)
            {
                yield return (await _registryService.DownloadLayer(_imageReference.Repository, layer.Digest), layer.Size, layer.Digest);
            }
        }

        private static DockerImageReference ExtractBaseImageReference(ImageConfig config)
        {
            var baseImageName = config.Config.Labels[Annotations.BaseImageNameAnnotation];
            var baseImageDigest = config.Config.Labels[Annotations.BaseImageDigestAnnotation];
            return DockerImageReference.Parse(baseImageName, baseImageDigest);
        }
        
        private async Task<Manifest> GetBaseImageManifestAsync()
        {
            return await _registryService.GetTagManifestAsync(_imageReference.Repository, _imageReference.Digest);
        }

        private async Task<ImageConfig> GetBaseImageConfigAsync()
        {
            var baseImageManifest = await GetBaseImageManifestAsync();
            return await _registryService.GetConfig(_imageReference.Repository, baseImageManifest.Config.Digest);
        }

        private async Task<Manifest> GetNewBaseImageManifestAsync()
        {
            return await _registryService.GetTagManifestAsync(_imageReference.Repository, _imageReference.Tag);
        }

        private async Task<ImageConfig> GetNewBaseImageConfigAsync()
        {
            var baseImageManifest = await GetNewBaseImageManifestAsync();
            return await _registryService.GetConfig(_imageReference.Repository, baseImageManifest.Config.Digest);
        }
    }
}
