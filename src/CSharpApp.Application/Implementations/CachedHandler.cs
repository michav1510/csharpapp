using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSharpApp.Application.Implementations
{
    public class CachedHandler: DelegatingHandler
    {
        private readonly IMemoryCache _cache;

        public CachedHandler(IMemoryCache cache)
        {
           _cache = cache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                CancellationToken cancellationToken)
        {
            var queryString = HttpUtility.ParseQueryString(request.RequestUri!.Query);
            var query = queryString["q"];
            var units = queryString["units"];
            var key = $"{query}-{units}";

            var cached = _cache.Get<string>(key);
            if(cached is not null)
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(cached)
                };
            }

            var response = await base.SendAsync(request, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            _cache.Set(key, content, TimeSpan.FromMinutes(1));
            return response;
        }
    }
}
