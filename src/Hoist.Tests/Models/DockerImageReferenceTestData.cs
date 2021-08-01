using Hoist.Models;
using Xunit;

namespace Hoist.Tests.Models
{
    public class DockerImageReferenceTestData : TheoryData<string, DockerImageReference>
    {
        public DockerImageReferenceTestData()
        {
            Add("ubuntu", new DockerImageReference { Host = "docker.io", Repository = "library/ubuntu", Tag = "latest" });
            Add("ubuntu:latest", new DockerImageReference { Host = "docker.io", Repository = "library/ubuntu", Tag = "latest" });
            Add("docker.io/ubuntu:latest", new DockerImageReference { Host = "docker.io", Repository = "library/ubuntu", Tag = "latest" });
            Add("docker.io/library/ubuntu:latest", new DockerImageReference { Host = "docker.io", Repository = "library/ubuntu", Tag = "latest" });
            Add("docker.io/library/ubuntu:18.04", new DockerImageReference { Host = "docker.io", Repository = "library/ubuntu", Tag = "18.04" });
            Add("mcr.microsoft.com/dotnet/sdk:5.0", new DockerImageReference { Host = "mcr.microsoft.com", Repository = "dotnet/sdk", Tag = "5.0" });
            Add("mcr.microsoft.com/hello-world", new DockerImageReference { Host = "mcr.microsoft.com", Repository = "hello-world", Tag = "latest" });
        }
    }
}
