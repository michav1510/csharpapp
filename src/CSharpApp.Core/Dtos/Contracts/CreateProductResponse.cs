using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class CreateProductResponse
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public IEnumerable<string>? Images { get; set; }

        public Category? Category { get; set; }

        public DateTime? CreationAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
