using Docker.Registry.DotNet.Authentication;
using Hoist.Models;
using Hoist.Models.Settings;

namespace Hoist.Utilities
{
    public static class RegistryUtilities
    {
        public static RegistryHost GetRegistryHost(string uri)
        {
            if (uri.Contains("azurecr.io"))
            {
                return RegistryHost.AzureContainerRegistry;
            }

            if (uri.Contains("ghcr.io"))
            {
                return RegistryHost.GitHubContainerRegistry;
            }

            return RegistryHost.DockerHub;
        }

        public static AuthenticationProvider CreateAuthenticationProvider(string host, DockerRegistrySettings settings)
        {
            AuthenticationProvider authenticationProvider = new AnonymousOAuthAuthenticationProvider();

            if (!settings.DockerRegistries.ContainsKey(host))
            {
                return authenticationProvider;
            }

            var username = settings.DockerRegistries[host].Username;
            var password = settings.DockerRegistries[host].Password;

            var registryHost = GetRegistryHost(host);
            authenticationProvider = registryHost switch
            {
                RegistryHost.AzureContainerRegistry => new OAuthAccessTokenAuthenticationProvider(username, password),
                RegistryHost.DockerHub => new PasswordOAuthAuthenticationProvider(username, password),
                _ => new AnonymousOAuthAuthenticationProvider()
            };

            return authenticationProvider;
        }

        public static string NormalizeRegistryHost(string host)
        {
            if (host.Contains("docker.io"))
            {
                return "registry.hub.docker.com";
            }

            if (host.Contains("mcr.microsoft.com"))
            {
                return "mcr.microsoft.com";
            }

            return host;
        }
    }
}
