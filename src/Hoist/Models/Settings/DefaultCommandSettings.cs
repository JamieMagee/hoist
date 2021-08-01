using System.ComponentModel;
using Spectre.Console.Cli;

namespace Hoist.Models.Settings
{
    public sealed class DefaultCommandSettings : CommandSettings
    {
        [CommandArgument(0, "<REGISTRY>")]
        [Description("The docker registry to use")]
        public string Registry { get; set; }

        [CommandOption("-r|--repository [REPOSITORY]")]
        [Description("The docker image repository to use")]
        public FlagValue<string> Repository { get; set; }

        [CommandOption("-t|--tag [TAG]")]
        [Description("The docker image tag to use")]
        public FlagValue<string> Tag { get; set; }
        
        [CommandOption("-o|--overwrite [OVERWRITE]")]
        [Description("Overwrite the existing tag")]
        public FlagValue<bool> Overwrite { get; set; }

        [CommandOption("-n|--base-name-label [BASE_NAME_LABEL]")]
        [Description("The base image name label to use. Defaults to org.opencontainers.image.base.name")]
        public FlagValue<string> BaseImageNameLabel { get; set; }

        [CommandOption("-d|--base-digest-label [BASE_DIGEST_LABEL]")]
        [Description("The base image digest label to use. Defaults to org.opencontainers.image.base.digest")]
        public FlagValue<string> BaseImageDigestLabel { get; set; }
    }
}
