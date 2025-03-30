using CSharpApp.Application.Products.Queries.GetAllProducts;
using CSharpApp.Core.Dtos.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Application.Implementations
{
    public class TypedClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;


        public TypedClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Ignore case for property names
                WriteIndented = false
            };
        }

        public async Task<IEnumerable<GetAllProductsResponse>> GetProductsAsync(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response =  await _httpClient.GetAsync("https://api.escuelajs.co/api/v1/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GetAllProductsResponse>>(json, _jsonOptions);
        }

    }
}
