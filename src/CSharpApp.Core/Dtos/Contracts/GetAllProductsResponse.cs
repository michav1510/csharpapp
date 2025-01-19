using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class GetAllProductsResponse
    {
        public IEnumerable<CreateProductResponse> AllProducts { get; set; }
    }
}
