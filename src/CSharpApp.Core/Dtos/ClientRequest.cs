using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos
{
    public abstract class ClientRequest
    {
        public virtual HttpMethod Method => HttpMethod.Get;

        public abstract string Path { get; }
    }
}
