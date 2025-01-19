using CSharpApp.Core.Dtos.Queries;
using CSharpApp.Core.Dtos.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResponse>
    {
        private readonly IMyClient _client;

        public GetAllProductsHandler(IMyClient client)
        {
            _client = client;
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _client.Request<GetAllProductsRequest, IEnumerable<CreateProductResponse>>(new GetAllProductsRequest());
            var getAllProductsresult = new GetAllProductsResponse { AllProducts = result };
            return getAllProductsresult;         
        }
    }
}
