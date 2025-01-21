//using CSharpApp.Application.Products;
using CSharpApp.Core.Dtos.Commands;
using CSharpApp.Core.Dtos.Queries;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Logging.ClearProviders().AddSerilog(logger);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDefaultConfiguration();
builder.Services.AddHttpConfiguration();
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

var versionedEndpointRouteBuilder = app.NewVersionedApi();

versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/getproducts", async (IMediator mediator) =>
{
    var products = await mediator.Send(new GetAllProductsQuery());
    return products;
})
    .WithName("GetProducts")
.HasApiVersion(1.0);


versionedEndpointRouteBuilder.MapPost("api/v{version:apiVersion}/createproduct", async (IMediator mediator, CreateProductCommand command) =>
{
    var result = await mediator.Send(command);  
    return result;
})
    .WithName("CreateProduct")
    .HasApiVersion(1.0);

app.Run();