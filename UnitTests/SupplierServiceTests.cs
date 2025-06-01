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
        [Test]
        public void GetAllSuppliers_WhenDaoReturnsEmptyList_ReturnsEmptyList()
        {
            _mockDAO.Setup(d => d.GetAllSuppliers()).Returns(new List<Supplier>());

            var result = _service.GetAllSuppliers();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void AddSupplier_WhenDaoReturnsNegativeOne_ReturnsNegativeOne()
        {
            var dto = new SupplierDTO { SupplierName = "ErrorProv", CategorySupply = 1 };
            _mockDAO.Setup(d => d.AddSupplier(It.IsAny<Supplier>())).Returns(-1);

            var result = _service.AddSupplier(dto);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void UpdateSupplier_WhenDaoReturnsFalse_ReturnsFalse()
        {
            var dto = new SupplierDTO { Id = 7, SupplierName = "UpdateFail" };
            _mockDAO.Setup(d => d.UpdateSupplier(It.IsAny<Supplier>())).Returns(false);

            var result = _service.UpdateSupplier(dto);

            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteSupplier_WhenDaoReturnsFalse_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.DeleteSupplier(9)).Returns(false);

            var result = _service.DeleteSupplier(9);

            Assert.IsFalse(result);
        }

        [Test]
        public void ReactivateSupplier_WhenDaoReturnsFalse_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.ReactivateSupplier(11)).Returns(false);

            var result = _service.ReactivateSupplier(11);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanDeleteSupplier_WhenDaoReturnsFalse_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.CanDeleteSupplier(13)).Returns(false);

            var result = _service.CanDeleteSupplier(13);

            Assert.IsFalse(result);
        }
        [Test]
        public void AddSupplier_WhenDaoThrowsException_ThrowsException()
        {
            var dto = new SupplierDTO { SupplierName = "Excepcional", CategorySupply = 2 };
            _mockDAO.Setup(d => d.AddSupplier(It.IsAny<Supplier>())).Throws(new System.Exception("DB error"));

            Assert.Throws<System.Exception>(() => _service.AddSupplier(dto));
        }

        [Test]
        public void GetAllSuppliers_WhenDaoThrowsException_ThrowsException()
        {
            _mockDAO.Setup(d => d.GetAllSuppliers()).Throws(new System.Exception("Falló conexión"));

            Assert.Throws<System.Exception>(() => _service.GetAllSuppliers());
        }

    }
}
