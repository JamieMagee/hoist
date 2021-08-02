# Demos

## Ubuntu

```shell
# Build image
docker build . -t hoist.azurecr.io/ubuntu:18.04
# Run image and see Ubuntu version
docker run --rm hoist.azurecr.io/ubuntu:18.04
# Scan image and see number of vulnerabilities
trivy image --severity "MEDIUM,HIGH,CRITICAL" hoist.azurecr.io/ubuntu:18.04
# Push image to registry
docker push
# Run hoist
hoist hoist.azurecr.io
# Run updated image and see Ubuntu version
docker run --rm hoist.azurecr.io/ubuntu:18.04-hoist
# Scan updated image and see number of vulnerabilities
trivy image --severity "MEDIUM,HIGH,CRITICAL" hoist.azurecr.io/ubuntu:18.04-hoist
```

## .NET

```shell
# Build image
docker build . -t hoist.azurecr.io/dotnet:latest
# Run image and see .NET version
docker run -p 8080:80 hoist.azurecr.io/dotnet:latest
http://localhost:8080
# Scan image and see number of vulnerabilities
trivy image --severity "MEDIUM,HIGH,CRITICAL" hoist.azurecr.io/dotnet:latest
# See security releases https://versionsof.net/core/5.0/
# Push image to registry
docker push
# Run hoist
hoist hoist.azurecr.io
# Run updated image and see .NET version
docker run -p 8080:80 hoist.azurecr.io/dotnet:latest-hoist
http://localhost:8080
# Scan image and see number of vulnerabilities
trivy image --severity "MEDIUM,HIGH,CRITICAL" hoist.azurecr.io/dotnet:latest-hoist
```