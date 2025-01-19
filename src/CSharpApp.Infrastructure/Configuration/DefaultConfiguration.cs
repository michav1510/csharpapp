using CSharpApp.Application.Handlers;
using CSharpApp.Application.Implementations;
using CSharpApp.Core.Dtos.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.Configure<RestApiSettings>(configuration!.GetSection(nameof(RestApiSettings)));
        services.Configure<HttpClientSettings>(configuration.GetSection(nameof(HttpClientSettings)));

        services.AddSingleton<IProductsService, ProductsService>();
        services.AddTransient<IMyClient, MyClient<IMyClient>>();
        services.AddHttpClient<IMyClient, MyClient<IMyClient>>();
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
        });


        return services;
    }
}