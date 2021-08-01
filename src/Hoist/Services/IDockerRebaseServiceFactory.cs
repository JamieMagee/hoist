using Hoist.Models.Registry.Config;
using Hoist.Models.Registry.Manifest;

namespace Hoist.Services
{
    public interface IDockerRebaseServiceFactory
    {
        IDockerRebaseService GetDockerRebaseService(Manifest manifest, ImageConfig imageConfig);
    }
}
