using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos.Contracts
{
    public class CreateProductResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("images")]
        public IEnumerable<string>? Images { get; set; }

        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("creationAt")]
        public DateTime? CreationAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }
    }
}
