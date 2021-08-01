using Hoist.Extensions;
using JetBrains.Annotations;

namespace Hoist.Models
{
    public sealed class DockerImageReference
    {
        public DockerImageReference()
        {
        }

        public string Host { get; set; }

        public string Repository { get; set; }

        public string Tag { get; set; }

        public string Digest { get; set; }

        public static DockerImageReference Parse(string reference, [CanBeNull] string digest)
        {
            var dockerImageReference = new DockerImageReference();
            var i = reference.IndexOf('/');
            if (i == -1 || !((reference[..i].Contains(':') || reference[..i].Contains('.')) && reference[..i] != "localhost"))
            {
                dockerImageReference.Host = "docker.io";
            }
            else
            {
                dockerImageReference.Host = reference[..i];
            }

            var (repository, tag, _) = reference[(i + 1)..].Split(':');
            if (dockerImageReference.Host == "docker.io" && !repository.Contains('/'))
            {
                dockerImageReference.Repository = $"library/{repository}";
            }
            else
            {
                dockerImageReference.Repository = repository;
            }
            
            dockerImageReference.Tag = tag ?? "latest";
            dockerImageReference.Digest = digest;

            return dockerImageReference;
        }
    }
}
