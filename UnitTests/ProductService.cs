using NUnit.Framework;
using Moq;
using Services;
using Model;
using Services.Dtos;
using System.Collections.Generic;

namespace UnitTests
{
    public class ProductServiceTests
    {
        private Mock<IProductDAO> _mockDAO;
        private ProductService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<IProductDAO>();
            _service = new ProductService(_mockDAO.Object);
        }

        [Test]
        public void AddProduct_NormalFlow_ReturnsProductID()
        {
            var productDTO = new ProductDTO
            {
                Name = "Pizza",
                Category = 1,
                Price = 10.99m,
                IsPrepared = false,
                ProductPic = null,
                Description = "Delicious",
                SupplyID = 1
            };

            _mockDAO.Setup(d => d.ExistsProductCode(It.IsAny<string>())).Returns(false);
            _mockDAO.Setup(d => d.AddProduct(It.IsAny<Product>())).Returns(new Product { ProductID = 1 });

            var result = _service.AddProduct(productDTO);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddProduct_AlternFlow_ReturnsMinusOne()
        {
            var productDTO = new ProductDTO
            {
                Name = "Pizza",
                Category = 1,
                Price = 10.99m,
                IsPrepared = false,
                ProductPic = null,
                Description = "Delicious",
                SupplyID = 1
            };

            _mockDAO.Setup(d => d.ExistsProductCode(It.IsAny<string>())).Returns(false);
            _mockDAO.Setup(d => d.AddProduct(It.IsAny<Product>())).Returns((Product)null);

            var result = _service.AddProduct(productDTO);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void GetAllProducts_NormalFlow_ReturnsList()
        {
            _mockDAO.Setup(d => d.GetAllProductsWithRecipe(false)).Returns(new List<Product>
            {
                new Product { ProductID = 1, Name = "Pizza", ProductCode = "P001", IsActive = true, IsPrepared = false }
            });

            _mockDAO.Setup(d => d.IsProductUsedInOrders(It.IsAny<int>())).Returns(false);

            var result = _service.GetAllProducts();

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void UpdateProduct_NormalFlow_ReturnsTrue()
        {
            var productDTO = new ProductDTO { ProductID = 1, Name = "Pizza", Category = 1, Price = 12, IsPrepared = true };

            _mockDAO.Setup(d => d.UpdateProduct(It.IsAny<Product>())).Returns(true);

            var result = _service.UpdateProduct(productDTO);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteProduct_NormalFlow_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.IsProductUsedInOrders(1)).Returns(false);
            _mockDAO.Setup(d => d.DeleteProduct(1)).Returns(true);

            var result = _service.DeleteProduct(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteProduct_AlternoFlow_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.IsProductUsedInOrders(1)).Returns(true);

            var result = _service.DeleteProduct(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ReactivateProduct_NormalFlow_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.ReactivateProduct(1)).Returns(true);

            var result = _service.ReactivateProduct(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsProductDeletable_True()
        {
            _mockDAO.Setup(d => d.IsProductUsedInOrders(1)).Returns(false);

            var result = _service.IsProductDeletable(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsProductDeletable_False()
        {
            _mockDAO.Setup(d => d.IsProductUsedInOrders(1)).Returns(true);

            var result = _service.IsProductDeletable(1);

            Assert.IsFalse(result);
        }
    }
}
