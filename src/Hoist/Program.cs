using Hoist.Commands;
using Hoist.Models.Settings;
using Hoist.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using Spectre.Console.Cli;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("hoist.json")
    .Build();
var serviceCollection = new ServiceCollection()
    .Configure<DockerRegistrySettings>(configuration)
    .AddLogging(configure =>
        configure.AddSimpleConsole(options =>
        {
            options.SingleLine = true;
            options.IncludeScopes = true;
        })
    )
    .AddSingleton<IDockerRegistryServiceFactory, DockerRegistryServiceFactory>()
    .AddSingleton<IDockerRebaseServiceFactory, DockerRebaseServiceFactory>();
using var registrar = new DependencyInjectionRegistrar(serviceCollection);
var app = new CommandApp<DefaultCommand>(registrar);
app.Configure(config =>
{
    config.CaseSensitivity(CaseSensitivity.None);
    config.SetApplicationName("hoist");
    config.AddExample(new[]
    {
        "hoist.azurecr.io"
    });
    config.AddExample(new[]
    {
        "hoist.azurecr.io",
        "--repository",
        "my-dotnet-app"
    });
    config.AddExample(new[]
    {
        "hoist.azurecr.io",
        "--repository",
        "my-dotnet-app",
        "--tag",
        "1.2.3"
    });
    config.AddExample(new[]
    {
        "hoist.azurecr.io",
        "--base-name-label",
        "image.base.ref.name",
        "--base-digest-label",
        "image.base.digest"
    });
    config.ValidateExamples();
});
return await app.RunAsync(args);
