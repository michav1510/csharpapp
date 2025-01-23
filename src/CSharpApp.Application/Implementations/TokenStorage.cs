namespace CSharpApp.Application.Implementations
{
    public class TokenStorage : ITokenStorage
    {
        private string? _token;
        private DateTime? _expires;
        private IDateTimeProvider _dateTimeProvider;
        
        public TokenStorage(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }


        public string GetToken()
        {
            return _token;
        }

        public void SaveToken(string token)
        {
            _token = token;
            _expires = _dateTimeProvider.Now.AddDays(20);
        }

        public bool IsTokenValid()
        {
            if(string.IsNullOrEmpty(_token))
            {
                return false;
            }
            DateTime now = _dateTimeProvider.Now;
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
