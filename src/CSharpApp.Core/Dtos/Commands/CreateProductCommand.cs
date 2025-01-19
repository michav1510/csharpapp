using CSharpApp.Core.Dtos.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Commands
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
