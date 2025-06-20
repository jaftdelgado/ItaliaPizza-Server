using NUnit.Framework;
using Moq;
using Services.OrderServices;
using Services.Dtos;
using System;
using System.Collections.Generic;
using Model;

namespace UnitTests
{
    public class OrderServiceTests
    {
        private Mock<IOrderDAO> _mockDAO;
        private OrderService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<IOrderDAO>();
            _service = new OrderService(_mockDAO.Object);
        }

        // --- GetOrders ---

        [Test]
        public void GetOrders_NormalFlow_ReturnsList()
        {
            var expectedList = new List<OrderDTO> { new OrderDTO { OrderID = 1 } };
            _mockDAO.Setup(d => d.GetOrders(It.IsAny<List<int>>(), true, false)).Returns(expectedList);

            var result = _service.GetOrders(new List<int> { 1 }, true, false);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetOrders_AlternateFlow_EmptyList()
        {
            _mockDAO.Setup(d => d.GetOrders(It.IsAny<List<int>>(), false, false)).Returns(new List<OrderDTO>());

            var result = _service.GetOrders(new List<int>(), false, false);

            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetOrders_WhenExceptionThrown_Throws()
        {
            _mockDAO.Setup(d => d.GetOrders(It.IsAny<List<int>>(), true, true)).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.GetOrders(new List<int> { 1 }, true, true));
        }

        // --- ChangeOrderStatus ---

        [Test]
        public void ChangeOrderStatus_NormalFlow_ReturnsTrue()
        {
            _mockDAO.Setup(d => d.ChangeOrderStatus(1, 2, 3)).Returns(true);

            var result = _service.ChangeOrderStatus(1, 2, 3);

            Assert.IsTrue(result);
        }

        [Test]
        public void ChangeOrderStatus_AlternateFlow_ReturnsFalse()
        {
            _mockDAO.Setup(d => d.ChangeOrderStatus(999, 2, 3)).Returns(false);

            var result = _service.ChangeOrderStatus(999, 2, 3);

            Assert.IsFalse(result);
        }

        [Test]
        public void ChangeOrderStatus_WhenExceptionThrown_Throws()
        {
            _mockDAO.Setup(d => d.ChangeOrderStatus(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.ChangeOrderStatus(1, 2, 3));
        }

        // --- AddLocalOrder ---

        [Test]
        public void AddLocalOrder_NormalFlow_ReturnsOne()
        {
            var dto = new OrderDTO { Total = 100, PersonalID = 1, TableNumber = "5", Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.AddLocalOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Returns(1);

            var result = _service.AddLocalOrder(dto);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddLocalOrder_AlternateFlow_ReturnsZero()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.AddLocalOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Returns(0);

            var result = _service.AddLocalOrder(dto);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void AddLocalOrder_WhenExceptionThrown_Throws()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.AddLocalOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.AddLocalOrder(dto));
        }

        // --- AddDeliveryOrder ---

        [Test]
        public void AddDeliveryOrder_NormalFlow_ReturnsOne()
        {
            var dto = new OrderDTO { CustomerID = 1, Total = 100, PersonalID = 1, Items = new List<ProductOrderDTO>() };
            var delivery = new DeliveryDTO { AddressID = 1, DeliveryDriverID = 1 };
            _mockDAO.Setup(d => d.AddDeliveryOrder(It.IsAny<Order>(), It.IsAny<Delivery>(), It.IsAny<List<Product_Order>>())).Returns(1);

            var result = _service.AddDeliveryOrder(dto, delivery);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddDeliveryOrder_AlternateFlow_ReturnsZero()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            var delivery = new DeliveryDTO();
            _mockDAO.Setup(d => d.AddDeliveryOrder(It.IsAny<Order>(), It.IsAny<Delivery>(), It.IsAny<List<Product_Order>>())).Returns(0);

            var result = _service.AddDeliveryOrder(dto, delivery);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void AddDeliveryOrder_WhenExceptionThrown_Throws()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            var delivery = new DeliveryDTO();
            _mockDAO.Setup(d => d.AddDeliveryOrder(It.IsAny<Order>(), It.IsAny<Delivery>(), It.IsAny<List<Product_Order>>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.AddDeliveryOrder(dto, delivery));
        }

        // --- UpdateOrder ---

        [Test]
        public void UpdateOrder_NormalFlow_ReturnsTrue()
        {
            var dto = new OrderDTO { OrderID = 1, Total = 50, Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.UpdateOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Returns(true);

            var result = _service.UpdateOrder(dto);

            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateOrder_AlternateFlow_ReturnsFalse()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.UpdateOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Returns(false);

            var result = _service.UpdateOrder(dto);

            Assert.IsFalse(result);
        }

        [Test]
        public void UpdateOrder_WhenExceptionThrown_Throws()
        {
            var dto = new OrderDTO { Items = new List<ProductOrderDTO>() };
            _mockDAO.Setup(d => d.UpdateOrder(It.IsAny<Order>(), It.IsAny<List<Product_Order>>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.UpdateOrder(dto));
        }
    }
}
