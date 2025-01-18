using CSharpApp.Application.Implementations;
using CSharpApp.Core.Dtos.Contracts;

namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly IMyClient _myClient;
    private readonly RestApiSettings _restApiSettings;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IOptions<RestApiSettings> restApiSettings, 
        ILogger<ProductsService> logger)
    {         
        _myClient = new MyClient<ProductsService>(new HttpClient(), logger);
        _restApiSettings = restApiSettings.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts()
    {
        List<Product> res = await _myClient.Request<GetAllProductsRequest, List<Product>>(new GetAllProductsRequest());        
        return res.AsReadOnly();      
    }
}