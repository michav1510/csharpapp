using CSharpApp.Core.Dtos.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Queries
{
    public class GetAllProductsQuery : IRequest<GetAllProductsResponse>
    {
    }
}
