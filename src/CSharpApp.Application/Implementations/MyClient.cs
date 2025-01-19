using System.Net;
using System.Text;

namespace CSharpApp.Application.Implementations
{
    public class MyClient<T> : IMyClient
    {
        private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private readonly HttpClient _httpClient;
        private readonly ILogger<T> _logger;

        public MyClient(HttpClient httpClient, ILogger<T> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<TResponse> Request<TRequest, TResponse>(TRequest request) where TRequest : ClientRequest
        {
            Uri uri = new($"https://api.escuelajs.co/{request.Path}");

            HttpResponseMessage? response;
            using (HttpRequestMessage requestMessage = new() { Method = request.Method, RequestUri = uri })
            {
                switch (request.Method.Method)
                {                    
                    case "GET":
                        break;
                    case "POST":
                        string requestContent = JsonSerializer.Serialize(request, _serializerOptions);
                        requestMessage.Content = new StringContent(JsonSerializer.Serialize(request, _serializerOptions), Encoding.UTF8, "application/json");
                        requestMessage.Headers.TryAddWithoutValidation("content-type", "application/json");
                        break;
                    default:
                        throw new InvalidOperationException($"Request method {request.Method} is not handled");
                }

                try
                {
                    response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Sending {nameof(TRequest)} failed");
                    return default;
                }
            }

            using HttpResponseMessage responseMessage = response;

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    string r = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<TResponse>(r, _serializerOptions);
                case HttpStatusCode.Created:
                    r = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<TResponse>(r, _serializerOptions);
                default:
                    _logger.LogError($"{nameof(TRequest)} failed with HTTP {responseMessage.StatusCode}: " + await responseMessage.Content.ReadAsStringAsync());
                    return default;

            }
        }
    }
}
