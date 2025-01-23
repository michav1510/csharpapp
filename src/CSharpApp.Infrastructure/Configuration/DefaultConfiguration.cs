using CSharpApp.Application.Handlers;
using CSharpApp.Application.Implementations;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.Configure<RestApiSettings>(configuration!.GetSection(nameof(RestApiSettings)));
        services.Configure<HttpClientSettings>(configuration.GetSection(nameof(HttpClientSettings)));

        var credStorage = new CredsStorage(configuration);

        services.AddSingleton<ICredsStorage>(credStorage);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IMyClient, MyClient<IMyClient>>();
        services.AddSingleton<ITokenStorage, TokenStorage>();
        services.AddHttpClient<IMyClient, MyClient<IMyClient>>();
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
            opt.RegisterServicesFromAssemblyContaining<GetAllProductsHandler>();
        });

        return services;
    }
}