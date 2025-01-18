using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Interfaces
{
    public interface IMyClient
    {
        Task<TResponse> Request<TRequest, TResponse>(TRequest request)
            where TRequest : ClientRequest;
    }

}
