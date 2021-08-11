FROM mcr.microsoft.com/dotnet/aspnet:5.0@sha256:c13c15d9dc8d46c698086f6ce09543dec6cc876ce0214dcc5694c7057b44e7f5 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0@sha256:a5688a79685a3ff51f379615b781acada298558fa1ef032819a69b2dfead1c75 AS build
WORKDIR /src
COPY ["WebApplication/WebApplication.csproj", "WebApplication/"]
RUN dotnet restore "WebApplication/WebApplication.csproj"
COPY . .
WORKDIR "/src/WebApplication"
RUN dotnet build "WebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

LABEL org.opencontainers.image.base.name=mcr.microsoft.com/dotnet/aspnet:5.0
LABEL org.opencontainers.image.base.digest=sha256:c13c15d9dc8d46c698086f6ce09543dec6cc876ce0214dcc5694c7057b44e7f5

ENTRYPOINT ["dotnet", "WebApplication.dll"]
