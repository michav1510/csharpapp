using CSharpApp.Core.Dtos.Contracts;
using MediatR;


namespace CSharpApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductHandler //: IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        //private readonly IMyClient _client;

        //public CreateProductHandler(IMyClient client)
        //{
        //    _client = client;
        //}

        //public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        //{
        //    var clientRequest = new CreateProductRequest(request.Title, request.Price, request.Description, request.CategoryId, request.Images);
        //    var result = await _client.Request<CreateProductRequest, CreateProductResponse>(clientRequest);
        //    return result;
        //}
    }
}
