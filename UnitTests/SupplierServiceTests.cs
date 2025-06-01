using NUnit.Framework;
using Moq;
using Services;
using Model;
using System.Collections.Generic;

namespace UnitTests
{
    public class SupplierServiceTests
    {
        private SupplierService _service;
        private Mock<ISupplierDAO> _mockDAO;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<ISupplierDAO>();
            _service = new SupplierService(_mockDAO.Object);
        }

        [Test]
        public void GetAllSuppliers_ReturnsMappedList()
        {
            var suppliers = new List<Supplier>
            {
                new Supplier { SupplierID = 1, SupplierName = "Proveedor A", IsActive = true }
            };
            _mockDAO.Setup(d => d.GetAllSuppliers()).Returns(suppliers);

            var result = _service.GetAllSuppliers();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Id, Is.EqualTo(1));
            Assert.That(result[0].SupplierName, Is.EqualTo("Proveedor A"));
        }

        [Test]
        public void AddSupplier_ValidDTO_ReturnsNewSupplierId()
        {
            var dto = new SupplierDTO { SupplierName = "Nuevo", CategorySupply = 1 };
            _mockDAO.Setup(d => d.AddSupplier(It.IsAny<Supplier>())).Returns(5);

            var result = _service.AddSupplier(dto);

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void UpdateSupplier_Success_ReturnsTrue()
        {
            var dto = new SupplierDTO { Id = 3, SupplierName = "Actualizado" };
            _mockDAO.Setup(d => d.UpdateSupplier(It.IsAny<Supplier>())).Returns(true);

            var result = _service.UpdateSupplier(dto);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteSupplier_SupplierExists_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.DeleteSupplier(1)).Returns(true);

            var result = _service.DeleteSupplier(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ReactivateSupplier_Success_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.ReactivateSupplier(2)).Returns(true);

            var result = _service.ReactivateSupplier(2);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanDeleteSupplier_NoPendingOrders_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.CanDeleteSupplier(1)).Returns(true);

            var result = _service.CanDeleteSupplier(1);

            Assert.IsTrue(result);
        }
    }
}
