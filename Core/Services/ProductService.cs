using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using System.ComponentModel.DataAnnotations;
using Zeiss_TakeHome.Domain.Entities;

namespace Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repo;
        private readonly IProductIdGenerator _productIdService;

        public ProductService(IGenericRepository<Product> repo, IProductIdGenerator productIdService)
        {
            _repo = repo;
            _productIdService = productIdService;
        }

        private Result<string> ValidateAndFormatProductId(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
                return Result<string>.Fail("Product ID cannot be empty.");

            productId = productId.Trim();

            if (!productId.All(char.IsDigit))
                return Result<string>.Fail("Product ID must contain only digits.");

            productId = productId.PadLeft(6, '0');

            if (productId.Length > 6)
                return Result<string>.Fail("Product ID cannot be longer than 6 digits.");

            return Result<string>.Success(productId);
        }

        public async Task<Result<Product>> CreateProductAsync(CreateProductDTO productDTO)
        {
            if (_repo.GetAsQueryable().Any(x => x.Name == productDTO.Name))
                return Result<Product>.Fail("Product with same name, already Exists");

            var product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                UnitPrice = productDTO.UnitPrice,
                Stock = productDTO.Stock
            };

            product.ProductId = await _productIdService.GenerateProductIdAsync();
            await _repo.AddAsync(product);          
            _repo.SaveChanges();
            return Result<Product>.Success(product);
        }

        public Result<Product> DeleteProduct(string productId)
        {
            var validatedId = ValidateAndFormatProductId(productId);
            if (!validatedId.IsSuccess)
                return Result<Product>.Fail(validatedId.Error);

            var productToBeDeleted = _repo.GetAsQueryable()
                                          .FirstOrDefault(x => x.ProductId == validatedId.Data);

            if (productToBeDeleted == default)
                return Result<Product>.Fail($"Product with Id: {validatedId.Data}, not found.");

            _repo.Delete(productToBeDeleted);
            _repo.SaveChanges();

            return Result<Product>.Success(productToBeDeleted);
        }

        public Result<List<Product>> GetAllProducts()
        {
            var products = _repo.GetAsQueryable().ToList();
            return Result<List<Product>>.Success(products);
        }

        public Result<Product> GetProductById([MaxLength(6)]string productId)
        {
            var validatedId = ValidateAndFormatProductId(productId);
            if (!validatedId.IsSuccess)
                return Result<Product>.Fail(validatedId.Error);

            var product = _repo.GetAsQueryable().FirstOrDefault(x => x.ProductId == validatedId.Data);

            if (product == default)
                return Result<Product>.Fail($"Product with Id: {validatedId.Data}, not found.");

            return Result<Product>.Success(product);
        }

        public async Task<Result<Product>> UpdateProductAsync(string productId, UpdateProductDTO productDTO)
        {
            var validatedId = ValidateAndFormatProductId(productId);
            if (!validatedId.IsSuccess)
                return Result<Product>.Fail(validatedId.Error);

            var product = _repo.GetAsQueryable().FirstOrDefault(x => x.ProductId == validatedId.Data);

            if (product == default)
                return Result<Product>.Fail($"Product with Id: {validatedId.Data}, not found.");

            product.Name = string.IsNullOrEmpty(productDTO.Name?.Trim()) ? product.Name : productDTO.Name;
            product.Description = string.IsNullOrEmpty(productDTO.Description?.Trim()) ? product.Description : productDTO.Description;
            product.UnitPrice = productDTO.UnitPrice == null ? product.UnitPrice : productDTO.UnitPrice.Value;

            await _repo.UpdateAsync(product);
            _repo.SaveChanges();

            return Result<Product>.Success(product);
        }

        public async Task<Result<Product>> UpdateProductStockAsync(string productId, bool isIncrement, int value)
        {
            var validatedId = ValidateAndFormatProductId(productId);
            if (!validatedId.IsSuccess)
                return Result<Product>.Fail(validatedId.Error);

            var product = _repo.GetAsQueryable().FirstOrDefault(x => x.ProductId == validatedId.Data);

            if (product == default)
                return Result<Product>.Fail($"Product with Id: {validatedId.Data}, not found.");

            if (isIncrement)
                product.Stock += value;
            else 
                product.Stock -= value;

            await _repo.UpdateAsync(product);
            _repo.SaveChanges();

            return Result<Product>.Success(product);
        }        
    }
}
