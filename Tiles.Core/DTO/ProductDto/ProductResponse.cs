using Newtonsoft.Json;

namespace Tiles.Core.DTO.ProductDto
{
    public class ProductResponse
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; } = null!;

        [JsonProperty("productName")]
        public string ProductName { get; set; } = null!;

        [JsonProperty("productImage")]
        public string ProductImage { get; set; } = null!;

        [JsonProperty("productSizes")]
        public List<string> ProductSizes { get; set; } = new();

        [JsonProperty("description")]
        public string Description { get; set; } = null!;

        [JsonProperty("colors")]
        public List<string> Colors { get; set; } = new();

        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; } = null!;

        // Change stock to string
        [JsonProperty("stock")]
        public string Stock { get; set; } = null!;

        [JsonProperty("link360")]
        public string? Link360 { get; set; }

        [JsonProperty("category")]
        public NestedCategory Category { get; set; } = null!;

        [JsonProperty("subCategory")]
        public NestedCategory SubCategory { get; set; } = null!;
    }

    public class NestedCategory
    {
        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = null!;
    }
}
