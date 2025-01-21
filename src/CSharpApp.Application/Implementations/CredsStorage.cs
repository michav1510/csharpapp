using Microsoft.Extensions.Configuration;

namespace CSharpApp.Application.Implementations
{
    public class CredsStorage : ICredsStorage
    {
        private readonly string _email;
        private readonly string _password;
        public CredsStorage(IConfiguration configuration)
        {
            _email = configuration["RestApiSettings:Username"];
            _password = configuration["RestApiSettings:Password"];
        }

        public string GetEmail()
        {
            return _email;
        }

        public string GetPassword()
        {
            return _password;
        }
    }
}
