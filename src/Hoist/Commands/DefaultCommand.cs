using System.Collections.Generic;
using System.Threading.Tasks;
using Hoist.Models;
using Hoist.Models.Settings;
using Hoist.Services;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Hoist.Commands
{
    public class DefaultCommand : AsyncCommand<DefaultCommandSettings>
    {
        private readonly ILogger<DefaultCommand> _logger;
        private readonly IDockerRebaseServiceFactory _rebaseServiceFactory;
        private readonly IDockerRegistryServiceFactory _registryServiceFactory;

        public DefaultCommand(IDockerRegistryServiceFactory registryServiceFactory, IDockerRebaseServiceFactory rebaseServiceFactory,
            ILogger<DefaultCommand> logger)
        {
            _logger = logger;
            _registryServiceFactory = registryServiceFactory;
            _rebaseServiceFactory = rebaseServiceFactory;
        }

        public override ValidationResult Validate(CommandContext context, DefaultCommandSettings settings)
        {
            return !_registryServiceFactory.HasCredentialsForRegistry(settings.Registry)
                ? ValidationResult.Error($"Cannot find credentials for registry {settings.Registry}")
                : ValidationResult.Success();
        }

        public override async Task<int> ExecuteAsync(CommandContext context, DefaultCommandSettings settings)
        {
            var baseImageNameLabel = settings.BaseImageNameLabel.IsSet
                ? settings.BaseImageNameLabel.Value
                : Annotations.BaseImageNameAnnotation;
            var baseImageDigestLabel = settings.BaseImageDigestLabel.IsSet
                ? settings.BaseImageDigestLabel.Value
                : Annotations.BaseImageDigestAnnotation;

            var registryService = _registryServiceFactory.GetDockerRegistryService(settings.Registry);
            var repositories = await GetRepositoryAsync(registryService, settings);

            foreach (var repository in repositories)
            {
                var tags = await GetTagsAsync(registryService, repository, settings);

                foreach (var tag in tags)
                {
                    using (_logger.BeginScope($"[{repository}:{tag}]"))
                    {
                        var manifest = await registryService.GetTagManifestAsync(repository, tag);
                        var config = await registryService.GetConfig(repository, manifest.Config.Digest);
                        var rebaseService = _rebaseServiceFactory.GetDockerRebaseService(manifest, config);

                        if (!rebaseService.CanRebase(baseImageNameLabel, baseImageDigestLabel))
                        {
                            _logger.LogWarning("Image is missing required labels");
                            continue;
                        }

                        if (!await rebaseService.IsNewBaseImageAvailableAsync())
                        {
                            _logger.LogWarning("No updated tag available");
                            continue;
                        }

                        _logger.LogInformation("Updated tag available");

                        await foreach (var (layer, size, digest) in rebaseService.DownloadNewLayersAsync())
                        {
                            _logger.LogInformation($"Uploading new layer with digest {digest}");
                            await registryService.UploadLayer(repository, size, layer, digest);
                        }

                        var newImageConfig = await rebaseService.GenerateNewImageConfig();

                        _logger.LogInformation("Uploading new config");
                        var (newConfigDigest, newConfigSize) = await registryService.UploadConfigAsync(repository, newImageConfig);

                        var newImageManifest = await rebaseService.GenerateNewImageManifest(newConfigDigest, newConfigSize);

                        _logger.LogInformation($"Uploading new manifest with tag {tag}-hoist");
                        await registryService.UploadManifest(repository, $"{tag}-hoist", newImageManifest);
                    }
                }

            }

            _logger.LogInformation("Exiting...");
            return await Task.FromResult(0);
        }

        private static async Task<IEnumerable<string>> GetRepositoryAsync(IDockerRegistryService service, DefaultCommandSettings settings)
        {
            return settings.Repository.IsSet
                ? new List<string> { settings.Repository.Value }
                : AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
                    .Title("Select a [green]repository[/]")
                    .AddChoiceGroup("Select all", await service.ListRepositoriesAsync()));
        }

        private static async Task<IEnumerable<string>> GetTagsAsync(IDockerRegistryService service, string repository,
            DefaultCommandSettings settings)
        {
            return settings.Tag.IsSet
                ? new List<string> { settings.Tag.Value }
                : AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
                    .Title("Select a [green]tag[/]")
                    .AddChoiceGroup($"Select all from [green]{repository}[/]", await service.ListRepositoryTagsAsync(repository)));
        }
    }
}
