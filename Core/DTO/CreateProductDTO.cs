using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class CreateProductDTO

    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Description of the product.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// Price set for each unit of the product.
        /// </summary>
        [JsonPropertyName("unit_price")]
        [Required]
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Count of the product in stock.
        /// </summary>
        [JsonPropertyName("stock")]
        [Required]
        public int Stock { get; set; } = 0;
    }

    public class UpdateProductDTO
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary>
        /// Description of the product.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// Price set for each unit of the product.
        /// </summary>
        [JsonPropertyName("unit_price")]
        public decimal? UnitPrice { get; set; }
    }
}
