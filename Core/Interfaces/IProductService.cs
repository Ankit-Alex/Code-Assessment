using Core.DTO;
using Core.Entities;
using Zeiss_TakeHome.Domain.Entities;

namespace Core.Interfaces;

/// <summary>
/// Service that provide functionality to Create/Update..etc a Product
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Get all product details in the database.
    /// </summary>
    /// <returns></returns>
    public Result<List<Product>> GetAllProducts();
    /// <summary>
    /// Get a specific product in the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Result with product details whose Id matches the param.</returns>
    public Result<Product> GetProductById(string productId);
    /// <summary>
    /// Create/Add a new entry for Product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public Task<Result<Product>> CreateProductAsync(CreateProductDTO product);
    /// <summary>
    /// Update an existing Product.
    /// </summary>
    /// <param name="id">Unique Identifier of the product to be updated.</param>
    /// <param name="product">Product details to be updated.</param>
    /// <returns></returns>
    public Task<Result<Product>> UpdateProductAsync(string productId, UpdateProductDTO product);
    /// <summary>
    /// Update the stock value of the Product.
    /// </summary>
    /// <param name="id">Unique Identifier of the product to be updated.</param>
    /// <param name="isIncrement">The stock value needs to be incremented to decremented.</param>
    /// <returns></returns>
    public Task<Result<Product>> UpdateProductStockAsync(string productId, bool isIncrement, int value);
    /// <summary>
    /// Delete an existing product.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Result<Product> DeleteProduct(string productId);
}
