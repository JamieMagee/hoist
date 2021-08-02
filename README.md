# Hoist

Rebase a Docker image onto a new base image.

## Options

```sh
USAGE:
    hoist <REGISTRY> [OPTIONS]

EXAMPLES:
    hoist hoist.azurecr.io
    hoist hoist.azurecr.io --repository my-dotnet-app
    hoist hoist.azurecr.io --repository my-dotnet-app --tag 1.2.3
    hoist hoist.azurecr.io --base-name-label image.base.ref.name --base-digest-label image.base.digest

ARGUMENTS:
    <REGISTRY>    The docker registry to use

OPTIONS:
    -h, --help                                     Prints help information
    -r, --repository [REPOSITORY]                  The docker image repository to use
    -t, --tag [TAG]                                The docker image tag to use
    -o, --overwrite [OVERWRITE]                    Overwrite the existing tag
    -n, --base-name-label [BASE_NAME_LABEL]        The base image name label to use. Defaults to org.opencontainers.image.base.name
    -d, --base-digest-label [BASE_DIGEST_LABEL]    The base image digest label to use. Defaults to org.opencontainers.image.base.digest
```

## Authentication

Authentication for each registry can be defined in a `hoist.json` file located beside the app. See [`hoist.sample.json`](src/Hoist/hoist.sample.json) for an example.