using CSharpApp.Core.Dtos.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class CreateProductRequest : ClientRequest
    {
        public override HttpMethod Method => HttpMethod.Post;
        public override string Path => "api/v1/products";

        public CreateProductRequest(string? title, decimal price, string? description, int categoryId, IEnumerable<string>? images) 
        {
            Title = title;
            Price = price;
            Description = description;
            CategoryId = categoryId;
            Images = images;
        }

        public string? Title { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<string>? Images { get; set; }
    }
}
