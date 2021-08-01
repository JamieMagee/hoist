using System;
using Hoist.Models.Settings;
using Hoist.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hoist.Services
{
    public class DockerRegistryServiceFactory : IDockerRegistryServiceFactory
    {
        private readonly IOptions<DockerRegistrySettings> _options;
        private readonly IServiceProvider _serviceProvider;

        public DockerRegistryServiceFactory(IServiceProvider serviceProvider, IOptions<DockerRegistrySettings> options)
        {
            _serviceProvider = serviceProvider;
            _options = options;
        }

        public bool HasCredentialsForRegistry(string host)
        {
            return _options.Value.DockerRegistries.ContainsKey(host);
        }

        public IDockerRegistryService GetDockerRegistryService(string host)
        {
            var authenticationProvider = RegistryUtilities.CreateAuthenticationProvider(host, _options.Value);
            var dockerRegistryService = new DockerRegistryService(
                _serviceProvider.GetService<ILogger<DockerRegistryService>>(),
                RegistryUtilities.NormalizeRegistryHost(host),
                authenticationProvider
            );

            return dockerRegistryService;
        }
    }
}
