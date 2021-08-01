using System.Collections.Generic;

namespace Hoist.Models.Settings
{
    public class DockerRegistrySettings
    {
        public IDictionary<string, Authentication> DockerRegistries { get; set; }
    }

    public class Authentication
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
