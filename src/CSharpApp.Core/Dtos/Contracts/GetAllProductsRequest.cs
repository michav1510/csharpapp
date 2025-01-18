using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class GetAllProductsRequest : ClientRequest
    {
        public override HttpMethod Method => HttpMethod.Get;
        public override string Path => "api/v1/products";

        public GetAllProductsRequest() { }
    }
}
