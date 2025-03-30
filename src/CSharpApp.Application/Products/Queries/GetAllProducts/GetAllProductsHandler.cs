using CSharpApp.Application.Implementations;
using CSharpApp.Core.Dtos.Contracts;
using MediatR;

namespace CSharpApp.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetAllProductsResponse>>
    {
        private readonly TypedClient _client;

        public GetAllProductsHandler(TypedClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _client.GetProductsAsync(request, cancellationToken);            
        }
    }
}
