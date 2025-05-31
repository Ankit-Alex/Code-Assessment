using Core.DTO;
using Core.Entities;
using Core.Interfaces;
using Core.Services;
using FluentAssertions;
using NSubstitute;
using Zeiss_TakeHome.Domain.Entities;

namespace Tests.Services
{
    public class ProductServiceTests
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IProductIdGenerator _productIdGenerator;
        private readonly ProductService _sut;

        public ProductServiceTests()
        {
            _repository = Substitute.For<IGenericRepository<Product>>();
            _productIdGenerator = Substitute.For<IProductIdGenerator>();
            _sut = new ProductService(_repository, _productIdGenerator);
        }

        [Fact]
        public async Task CreateProductAsync_WithValidProduct_ShouldReturnSuccess()
        {
            // Arrange
            var productDto = new CreateProductDTO
            {
                Name = "Test Product",
                Description = "Test Description",
                UnitPrice = 10.99m,
                Stock = 100
            };

            _productIdGenerator.GenerateProductIdAsync().Returns("000123");
            _repository.GetAsQueryable().Returns(new List<Product>().AsQueryable());

            // Act
            var result = await _sut.CreateProductAsync(productDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Name.Should().Be(productDto.Name);
            result.Data.ProductId.Should().Be("000123");
            
            await _repository.Received(1).AddAsync(Arg.Any<Product>());
            _repository.Received(1).SaveChanges();
        }

        [Fact]
        public async Task CreateProductAsync_WithDuplicateName_ShouldReturnFailure()
        {
            // Arrange
            var existingProducts = new List<Product>
            {
                new Product { Name = "Test Product" }
            };

            var productDto = new CreateProductDTO { Name = "Test Product" };
            _repository.GetAsQueryable().Returns(existingProducts.AsQueryable());

            // Act
            var result = await _sut.CreateProductAsync(productDto);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Product with same name, already Exists");
            await _repository.DidNotReceive().AddAsync(Arg.Any<Product>());
        }

        [Theory]
        [InlineData("123", "000123")]
        [InlineData("000123", "000123")]
        [InlineData("1", "000001")]
        public void GetProductById_WithValidId_ShouldPadAndReturnProduct(string inputId, string expectedId)
        {
            // Arrange
            var product = new Product { ProductId = expectedId, Name = "Test" };
            _repository.GetAsQueryable().Returns(new List<Product> { product }.AsQueryable());

            // Act
            var result = _sut.GetProductById(inputId);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.ProductId.Should().Be(expectedId);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void GetProductById_WithInvalidId_ShouldReturnFailure(string invalidId)
        {
            // Act
            var result = _sut.GetProductById(invalidId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Product ID cannot be empty.");
        }

        [Fact]
        public void GetProductById_WithNonExistentId_ShouldReturnFailure()
        {
            // Arrange
            _repository.GetAsQueryable().Returns(new List<Product>().AsQueryable());

            // Act
            var result = _sut.GetProductById("123456");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Product with Id: 123456, not found.");
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("12a")]
        [InlineData("a12")]
        public void GetProductById_WithNonNumericId_ShouldReturnFailure(string invalidId)
        {
            // Act
            var result = _sut.GetProductById(invalidId);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Product ID must contain only digits.");
        }

        [Fact]
        public async Task UpdateProductStockAsync_WhenIncrementing_ShouldIncreaseStock()
        {
            // Arrange
            var product = new Product { ProductId = "000123", Stock = 10 };
            _repository.GetAsQueryable().Returns(new List<Product> { product }.AsQueryable());

            // Act
            var result = await _sut.UpdateProductStockAsync("123", true, 5);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Stock.Should().Be(15);
            await _repository.Received(1).UpdateAsync(Arg.Any<Product>());
        }

        [Fact]
        public async Task UpdateProductStockAsync_WhenDecrementing_ShouldDecreaseStock()
        {
            // Arrange
            var product = new Product { ProductId = "000123", Stock = 10 };
            _repository.GetAsQueryable().Returns(new List<Product> { product }.AsQueryable());

            // Act
            var result = await _sut.UpdateProductStockAsync("123", false, 5);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Stock.Should().Be(5);
            await _repository.Received(1).UpdateAsync(Arg.Any<Product>());
        }

        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = "000123", Name = "Product 1" },
                new Product { ProductId = "000124", Name = "Product 2" }
            };
            _repository.GetAsQueryable().Returns(products.AsQueryable());

            // Act
            var result = _sut.GetAllProducts();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Data.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task UpdateProductAsync_WithValidData_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product 
            { 
                ProductId = "000123", 
                Name = "Old Name",
                Description = "Old Description",
                UnitPrice = 10.0m
            };
            _repository.GetAsQueryable().Returns(new List<Product> { product }.AsQueryable());

            var updateDto = new UpdateProductDTO
            {
                Name = "New Name",
                Description = "New Description",
                UnitPrice = 20.0m
            };

            // Act
            var result = await _sut.UpdateProductAsync("123", updateDto);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Data.Name.Should().Be("New Name");
            result.Data.Description.Should().Be("New Description");
            result.Data.UnitPrice.Should().Be(20.0m);
            await _repository.Received(1).UpdateAsync(Arg.Any<Product>());
        }
    }
} 