using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Zeiss_TakeHome.Domain.Entities
{    
    public class Product: BaseAuditableEntity
    {        
        /// <summary>
        /// Primary Key for the product table.
        /// </summary>
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Unique 6-Digit Identifier for the product.
        /// </summary>
        [MaxLength(6)]
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; }
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
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Count of the product in stock.
        /// </summary>
        [JsonPropertyName("stock_available")]
        public int Stock { get; set; } = 0;
    }
}
