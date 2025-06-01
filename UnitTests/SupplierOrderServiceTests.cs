using Moq;
using NUnit.Framework;
using Services;
using Services.Dtos;
using Model;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class SupplierOrderServiceTests
    {
        private Mock<ISupplierOrderDAO> _mockDAO;
        private SupplierOrderService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<ISupplierOrderDAO>();
            _service = new SupplierOrderService(_mockDAO.Object);
        }

        [Test]
        public void GetAllSupplierOrders_ReturnsMappedList()
        {
            _mockDAO.Setup(d => d.GetAllSupplierOrders()).Returns(new List<SupplierOrder>
            {
                new SupplierOrder
                {
                    SupplierOrderID = 1,
                    SupplierID = 2,
                    OrderedDate = DateTime.Today,
                    Total = 100,
                    OrderFolio = "F001",
                    Supplier = new Supplier { SupplierName = "Proveedor", CategorySupply = 1 },
                    SupplierOrder_Supply = new List<SupplierOrder_Supply>()
                }
            });

            var result = _service.GetAllSupplierOrders();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].SupplierOrderID, Is.EqualTo(1));
        }

        [Test]
        public void AddSupplierOrder_ValidOrder_ReturnsOne()
        {
            var dto = new SupplierOrderDTO
            {
                SupplierID = 1,
                OrderedDate = DateTime.Today,
                Total = 50,
                Items = new List<SupplierOrderDTO.OrderItemDTO>
                {
                    new SupplierOrderDTO.OrderItemDTO { SupplyID = 1, Quantity = 2, Subtotal = 10 }
                }
            };

            _mockDAO.Setup(d => d.FolioExists(It.IsAny<string>())).Returns(false);
            _mockDAO.Setup(d => d.AddSupplierOrder(It.IsAny<SupplierOrder>(), It.IsAny<List<SupplierOrder_Supply>>())).Returns(1);

            var result = _service.AddSupplierOrder(dto);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void UpdateSupplierOrder_WhenDaoReturnsTrue_ReturnsTrue()
        {
            var dto = new SupplierOrderDTO
            {
                SupplierOrderID = 10,
                Total = 150,
                Items = new List<SupplierOrderDTO.OrderItemDTO>
                {
                    new SupplierOrderDTO.OrderItemDTO { SupplyID = 3, Quantity = 5, Subtotal = 75 }
                }
            };

            _mockDAO.Setup(d => d.UpdateSupplierOrder(It.IsAny<SupplierOrder>(), It.IsAny<List<SupplierOrder_Supply>>())).Returns(true);

            var result = _service.UpdateSupplierOrder(dto);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeliverOrder_Success_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.DeliverOrder(4)).Returns(true);

            var result = _service.DeliverOrder(4);

            Assert.IsTrue(result);
        }

        [Test]
        public void CancelSupplierOrder_Success_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.CancelSupplierOrder(7)).Returns(true);

            var result = _service.CancelSupplierOrder(7);

            Assert.IsTrue(result);
        }

        [Test]
        public void AddSupplierOrder_WhenDaoReturnsZero_ReturnsZero()
        {
            var dto = new SupplierOrderDTO
            {
                SupplierID = 2,
                OrderedDate = DateTime.Today,
                Total = 80,
                Items = new List<SupplierOrderDTO.OrderItemDTO>
                {
                    new SupplierOrderDTO.OrderItemDTO { SupplyID = 2, Quantity = 1, Subtotal = 80 }
                }
            };

            _mockDAO.Setup(d => d.FolioExists(It.IsAny<string>())).Returns(false);
            _mockDAO.Setup(d => d.AddSupplierOrder(It.IsAny<SupplierOrder>(), It.IsAny<List<SupplierOrder_Supply>>())).Returns(0);

            var result = _service.AddSupplierOrder(dto);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void DeliverOrder_WhenExceptionThrown_ThrowsException()
        {
            _mockDAO.Setup(d => d.DeliverOrder(It.IsAny<int>())).Throws(new Exception("DB error"));

            Assert.Throws<Exception>(() => _service.DeliverOrder(1));
        }

        [Test]
        public void CancelSupplierOrder_WhenExceptionThrown_ThrowsException()
        {
            _mockDAO.Setup(d => d.CancelSupplierOrder(It.IsAny<int>())).Throws(new Exception("Failure"));

            Assert.Throws<Exception>(() => _service.CancelSupplierOrder(3));
        }
    }
}
