using NUnit.Framework;
using Moq;
using Services.FinanceServices;
using Services.Dtos;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class FinanceServiceTests
    {
        private Mock<IFinanceDAO> _daoMock;
        private FinanceService _service;

        [SetUp]
        public void Setup()
        {
            _daoMock = new Mock<IFinanceDAO>();
            _service = new FinanceService(_daoMock.Object);
        }

        [Test]
        public void GetCurrentTransactions_Normal_ReturnsList()
        {
            _daoMock.Setup(d => d.GetCurrentTransactions()).Returns(new List<TransactionDTO> { new TransactionDTO() });

            var result = _service.GetCurrentTransactions();

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void GetCurrentTransactions_Empty_ReturnsEmptyList()
        {
            _daoMock.Setup(d => d.GetCurrentTransactions()).Returns(new List<TransactionDTO>());

            var result = _service.GetCurrentTransactions();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetCurrentTransactions_Exception_Throws()
        {
            _daoMock.Setup(d => d.GetCurrentTransactions()).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.GetCurrentTransactions());
        }

        [Test]
        public void RegisterOrderPayment_Success_ReturnsTrue()
        {
            _daoMock.Setup(d => d.RegisterOrderPayment(1, 100)).Returns(1);

            var result = _service.RegisterOrderPayment(1, 100);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void RegisterOrderPayment_Invalid_ReturnsFalse()
        {
            _daoMock.Setup(d => d.RegisterOrderPayment(99, 50)).Returns(-1);

            var result = _service.RegisterOrderPayment(99, 50);

            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        public void RegisterOrderPayment_Exception_Throws()
        {
            _daoMock.Setup(d => d.RegisterOrderPayment(It.IsAny<int>(), It.IsAny<decimal>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.RegisterOrderPayment(1, 100));
        }

        [Test]
        public void GetOpenCashRegisterInfo_ReturnsData()
        {
            _daoMock.Setup(d => d.GetOpenCashRegisterInfo()).Returns(new CashRegisterDTO { InitialBalance = 100 });

            var result = _service.GetOpenCashRegisterInfo();

            Assert.That(result.InitialBalance, Is.EqualTo(100));
        }

        [Test]
        public void GetOpenCashRegisterInfo_NoOpen_ReturnsNull()
        {
            _daoMock.Setup(d => d.GetOpenCashRegisterInfo()).Returns((CashRegisterDTO)null);

            var result = _service.GetOpenCashRegisterInfo();

            Assert.IsNull(result);
        }

        [Test]
        public void GetOpenCashRegisterInfo_Exception_Throws()
        {
            _daoMock.Setup(d => d.GetOpenCashRegisterInfo()).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.GetOpenCashRegisterInfo());
        }

        [Test]
        public void OpenCashRegister_Success_ReturnsTrue()
        {
            _daoMock.Setup(d => d.OpenCashRegister(200)).Returns(true);

            Assert.IsTrue(_service.OpenCashRegister(200));
        }

        [Test]
        public void OpenCashRegister_AlreadyOpen_ReturnsFalse()
        {
            _daoMock.Setup(d => d.OpenCashRegister(It.IsAny<decimal>())).Returns(false);

            Assert.IsFalse(_service.OpenCashRegister(50));
        }

        [Test]
        public void OpenCashRegister_Exception_Throws()
        {
            _daoMock.Setup(d => d.OpenCashRegister(It.IsAny<decimal>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.OpenCashRegister(100));
        }

        [Test]
        public void CloseCashRegister_Success_ReturnsTrue()
        {
            _daoMock.Setup(d => d.CloseCashRegister(300)).Returns(true);

            Assert.IsTrue(_service.CloseCashRegister(300));
        }

        [Test]
        public void CloseCashRegister_NoOpen_ReturnsFalse()
        {
            _daoMock.Setup(d => d.CloseCashRegister(It.IsAny<decimal>())).Returns(false);

            Assert.IsFalse(_service.CloseCashRegister(0));
        }

        [Test]
        public void CloseCashRegister_Exception_Throws()
        {
            _daoMock.Setup(d => d.CloseCashRegister(It.IsAny<decimal>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.CloseCashRegister(150));
        }

        [Test]
        public void RegisterCashOut_Success_Returns1()
        {
            _daoMock.Setup(d => d.RegisterCashOut(100, "Retiro")).Returns(1);

            var result = _service.RegisterCashOut(100, "Retiro");

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void RegisterCashOut_InsufficientBalance_ReturnsMinus2()
        {
            _daoMock.Setup(d => d.RegisterCashOut(It.IsAny<decimal>(), It.IsAny<string>())).Returns(-2);

            var result = _service.RegisterCashOut(9999, "Mucho");

            Assert.That(result, Is.EqualTo(-2));
        }

        [Test]
        public void RegisterCashOut_Exception_Throws()
        {
            _daoMock.Setup(d => d.RegisterCashOut(It.IsAny<decimal>(), It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.RegisterCashOut(1, "Error"));
        }

        [Test]
        public void RegisterSupplierOrderExpense_Success_Returns1()
        {
            _daoMock.Setup(d => d.RegisterSupplierOrderExpense(5)).Returns(1);

            var result = _service.RegisterSupplierOrderExpense(5);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void RegisterSupplierOrderExpense_NoCashRegister_ReturnsMinus2()
        {
            _daoMock.Setup(d => d.RegisterSupplierOrderExpense(It.IsAny<int>())).Returns(-2);

            var result = _service.RegisterSupplierOrderExpense(3);

            Assert.That(result, Is.EqualTo(-2));
        }

        [Test]
        public void RegisterSupplierOrderExpense_Exception_Throws()
        {
            _daoMock.Setup(d => d.RegisterSupplierOrderExpense(It.IsAny<int>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.RegisterSupplierOrderExpense(1));
        }

        [Test]
        public void HasOpenCashRegister_Open_ReturnsTrue()
        {
            _daoMock.Setup(d => d.HasOpenCashRegister()).Returns(true);

            Assert.IsTrue(_service.HasOpenCashRegister());
        }

        [Test]
        public void HasOpenCashRegister_Closed_ReturnsFalse()
        {
            _daoMock.Setup(d => d.HasOpenCashRegister()).Returns(false);

            Assert.IsFalse(_service.HasOpenCashRegister());
        }

        [Test]
        public void HasOpenCashRegister_Exception_Throws()
        {
            _daoMock.Setup(d => d.HasOpenCashRegister()).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.HasOpenCashRegister());
        }
    }
}
