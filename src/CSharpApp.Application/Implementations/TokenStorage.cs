namespace CSharpApp.Application.Implementations
{
    public class TokenStorage : ITokenStorage
    {
        private string? _token;
        private DateTime? _expires;
        
        public string GetToken()
        {
            return _token;
        }

        public void SaveToken(string token)
        {
            _token = token;
            _expires = DateTime.UtcNow.AddDays(20);
        }

        public bool IsTokenValid()
        {
            if(string.IsNullOrEmpty(_token))
            {
                return false;
            }
            DateTime now = DateTime.UtcNow;
            if (now < _expires)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
