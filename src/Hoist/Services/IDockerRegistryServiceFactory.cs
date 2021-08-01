namespace Hoist.Services
{
    public interface IDockerRegistryServiceFactory
    {
        public IDockerRegistryService GetDockerRegistryService(string host);

        bool HasCredentialsForRegistry(string host);
    }
}
