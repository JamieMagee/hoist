namespace Hoist.Models.Registry
{
    public static class MediaTypes
    {
        public const string DockerIndex = "application/vnd.docker.distribution.manifest.list.v2+json";
        
        public const string DockerManifest = "application/vnd.docker.distribution.manifest.v2+json";

        public const string DockerConfig = "application/vnd.docker.container.image.v1+json";

        public const string DockerLayer = "application/vnd.docker.image.rootfs.diff.tar.gzip";

        public const string OciIndex = "application/vnd.oci.image.index.v1+json";
    }
}
