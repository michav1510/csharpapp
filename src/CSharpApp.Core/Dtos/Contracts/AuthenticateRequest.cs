using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class AuthenticateRequest : ClientRequest
    {
        public override HttpMethod Method => HttpMethod.Post;
        public override string Path => "api/v1/auth/login";

        public AuthenticateRequest(string? email, string? password)
        {
            Email = email;
            Password = password;
        }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
