using CSharpApp.Application.Implementations;
using CSharpApp.Application.Products.Commands.CreateProduct;
using CSharpApp.Application.Products.Queries.GetAllProducts;

namespace CSharpApp.Infrastructure.Configuration;

public static class DefaultConfiguration
{
    public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.Configure<RestApiSettings>(configuration!.GetSection(nameof(RestApiSettings)));
        services.Configure<HttpClientSettings>(configuration.GetSection(nameof(HttpClientSettings)));

        services.AddSingleton<CachedHandler>();
        services.AddHttpClient<TypedClient>()
                    .AddHttpMessageHandler<CachedHandler>();
        services.AddMediatR(opt =>
        {
            //opt.RegisterServicesFromAssemblyContaining<CreateProductHandler>();
            opt.RegisterServicesFromAssemblyContaining<GetAllProductsHandler>();
        });

        return services;
    }
}