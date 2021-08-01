using System;
using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Hoist.Services
{
    public class DockerRebaseServiceFactory : IDockerRebaseServiceFactory
    {
        private readonly ILogger<DockerRebaseServiceFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DockerRebaseServiceFactory(IServiceProvider serviceProvider, ILogger<DockerRebaseServiceFactory> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public IDockerRebaseService GetDockerRebaseService(Manifest manifest, ImageConfig imageConfig)
        {
            return new DockerRebaseService(
                _serviceProvider.GetService<IDockerRegistryServiceFactory>(),
                manifest,
                imageConfig,
                _serviceProvider.GetService<ILogger<DockerRebaseService>>());
        }
    }
}
