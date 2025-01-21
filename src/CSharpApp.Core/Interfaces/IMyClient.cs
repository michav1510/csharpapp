using System;
namespace CSharpApp.Core.Interfaces
{
    public interface IMyClient
    {
        Task<TResponse> Request<TRequest, TResponse>(TRequest request)
            where TRequest : ClientRequest;
    }

}
