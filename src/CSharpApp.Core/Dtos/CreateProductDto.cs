using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpApp.Core.Dtos
{
    public sealed class CreateProductDto
    {
        [JsonPropertyName("title")]
        public int? Title { get; set; }

        [JsonPropertyName("price")]
        public int? Price { get; set; }

        [JsonPropertyName("description")]
        public int? Description { get; set; }

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("images")]
        public IEnumerable<string>? Images { get; set; }
    }
}
