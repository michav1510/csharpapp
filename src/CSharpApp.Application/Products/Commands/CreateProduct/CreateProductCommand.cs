using CSharpApp.Core.Dtos.Contracts;
using MediatR;


namespace CSharpApp.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public string? Title { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<string>? Images { get; set; }
    }
}
