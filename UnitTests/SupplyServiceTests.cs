using NUnit.Framework;
using Moq;
using Services.SupplyServices;
using Services.Dtos;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class SupplyServiceTests
    {
        private Mock<ISupplyDAO> _mockDAO;
        private SupplyService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<ISupplyDAO>();
            _service = new SupplyService(_mockDAO.Object);
        }

        [Test]
        public void GetSuppliesBySupplier_ReturnsList()
        {
            var supplies = new List<Supply>
            {
                new Supply { SupplyID = 1, SupplyName = "Tomate", Price = 10, Stock = 5 }
            };
            _mockDAO.Setup(d => d.GetSuppliesBySupplier(1)).Returns(supplies);

            var result = _service.GetSuppliesBySupplier(1);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Tomate"));
        }

        [Test]
        public void GetSuppliesAvailableByCategory_ReturnsList()
        {
            var supplies = new List<Supply>
            {
                new Supply { SupplyID = 1, SupplyName = "Queso", SupplyCategoryID = 2 }
            };
            _mockDAO.Setup(d => d.GetSuppliesAvailableByCategory(2, null)).Returns(supplies);

            var result = _service.GetSuppliesAvailableByCategory(2, null);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Queso"));
        }

        [Test]
        public void GetAllSupplies_ActiveOnlyTrue_ReturnsList()
        {
            _mockDAO.Setup(d => d.GetAllSupplies(true))
                .Returns(new List<SupplyDTO> { new SupplyDTO { Id = 1, Name = "Aceite" } });

            var result = _service.GetAllSupplies(true);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllSuppliesPage_ReturnsPagedList()
        {
            var fullList = Enumerable.Range(1, 10).Select(i => new SupplyDTO { Id = i, Name = $"Item{i}" }).ToList();
            _mockDAO.Setup(d => d.GetAllSupplies(false)).Returns(fullList);

            var result = _service.GetAllSuppliesPage(2, 3); // Page 2, 3 items per page => items 4,5,6

            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.First().Id, Is.EqualTo(4));
        }

        [Test]
        public void UpdateSupply_Valid_ReturnsTrue()
        {
            var dto = new SupplyDTO { Id = 1, Name = "Mozzarella", Price = 50 };
            _mockDAO.Setup(d => d.UpdateSupply(It.IsAny<Supply>())).Returns(true);

            var result = _service.UpdateSupply(dto);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateSupply_Failure_ReturnsFalse()
        {
            var dto = new SupplyDTO { Id = 1 };
            _mockDAO.Setup(d => d.UpdateSupply(It.IsAny<Supply>())).Returns(false);

            var result = _service.UpdateSupply(dto);

            Assert.IsFalse(result);
        }

        [Test]
        public void AssignSupplierToSupply_ValidList_ReturnsTrue()
        {
            var ids = new List<int> { 1, 2 };
            _mockDAO.Setup(d => d.AssignSupplierToSupply(ids, 1)).Returns(true);

            var result = _service.AssignSupplierToSupply(ids, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void UnassignSupplierFromSupply_ValidList_ReturnsTrue()
        {
            var ids = new List<int> { 3, 4 };
            _mockDAO.Setup(d => d.UnassignSupplierFromSupply(ids, 2)).Returns(true);

            var result = _service.UnassignSupplierFromSupply(ids, 2);

            Assert.IsTrue(result);
        }

        [Test]
        public void IsSupplyDeletable_True_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.IsSupplyDeletable(5)).Returns(true);

            var result = _service.IsSupplyDeletable(5);

            Assert.IsTrue(result);
        }
    }
}
