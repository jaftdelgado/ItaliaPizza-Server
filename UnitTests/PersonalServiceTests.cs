using NUnit.Framework;
using Moq;
using Services;
using Services.Dtos;
using Model;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class PersonalServiceTests
    {
        private Mock<IPersonalDAO> _daoMock;
        private PersonalService _service;

        [SetUp]
        public void Setup()
        {
            _daoMock = new Mock<IPersonalDAO>();
            _service = new PersonalService(_daoMock.Object);
        }

        [Test]
        public void GetAllPersonals_Normal_ReturnsList()
        {
            _daoMock.Setup(d => d.GetAllPersonals()).Returns(new List<Personal>
            {
                new Personal { PersonalID = 1, FirstName = "Ana", Address = new Address() }
            });

            var result = _service.GetAllPersonals();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPersonals_Empty_ReturnsEmptyList()
        {
            _daoMock.Setup(d => d.GetAllPersonals()).Returns(new List<Personal>());

            var result = _service.GetAllPersonals();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetAllPersonals_Exception_Throws()
        {
            _daoMock.Setup(d => d.GetAllPersonals()).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.GetAllPersonals());
        }

        [Test]
        public void AddPersonal_Valid_Returns1()
        {
            var dto = new PersonalDTO { Address = new AddressDTO() };
            _daoMock.Setup(d => d.AddPersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Returns(1);

            var result = _service.AddPersonal(dto);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AddPersonal_Failure_Returns0()
        {
            var dto = new PersonalDTO { Address = new AddressDTO() };
            _daoMock.Setup(d => d.AddPersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Returns(0);

            var result = _service.AddPersonal(dto);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void AddPersonal_Exception_Throws()
        {
            _daoMock.Setup(d => d.AddPersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.AddPersonal(new PersonalDTO { Address = new AddressDTO() }));
        }

        [Test]
        public void UpdatePersonal_Success_ReturnsTrue()
        {
            var dto = new PersonalDTO { Address = new AddressDTO() };
            _daoMock.Setup(d => d.UpdatePersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Returns(true);

            Assert.IsTrue(_service.UpdatePersonal(dto));
        }

        [Test]
        public void UpdatePersonal_NotFound_ReturnsFalse()
        {
            _daoMock.Setup(d => d.UpdatePersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Returns(false);

            Assert.IsFalse(_service.UpdatePersonal(new PersonalDTO { Address = new AddressDTO() }));
        }

        [Test]
        public void UpdatePersonal_Exception_Throws()
        {
            _daoMock.Setup(d => d.UpdatePersonal(It.IsAny<Personal>(), It.IsAny<Address>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.UpdatePersonal(new PersonalDTO { Address = new AddressDTO() }));
        }

        [Test]
        public void DeletePersonal_Success_ReturnsTrue()
        {
            _daoMock.Setup(d => d.DeletePersonal(1)).Returns(true);

            Assert.IsTrue(_service.DeletePersonal(1));
        }

        [Test]
        public void DeletePersonal_NotFound_ReturnsFalse()
        {
            _daoMock.Setup(d => d.DeletePersonal(1)).Returns(false);

            Assert.IsFalse(_service.DeletePersonal(1));
        }

        [Test]
        public void DeletePersonal_Exception_Throws()
        {
            _daoMock.Setup(d => d.DeletePersonal(It.IsAny<int>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.DeletePersonal(1));
        }

        [Test]
        public void ReactivatePersonal_Success_ReturnsTrue()
        {
            _daoMock.Setup(d => d.ReactivatePersonal(1)).Returns(true);

            Assert.IsTrue(_service.ReactivatePersonal(1));
        }

        [Test]
        public void ReactivatePersonal_Invalid_ReturnsFalse()
        {
            _daoMock.Setup(d => d.ReactivatePersonal(1)).Returns(false);

            Assert.IsFalse(_service.ReactivatePersonal(1));
        }

        [Test]
        public void ReactivatePersonal_Exception_Throws()
        {
            _daoMock.Setup(d => d.ReactivatePersonal(It.IsAny<int>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.ReactivatePersonal(1));
        }

        [Test]
        public void IsUsernameAvailable_Yes_ReturnsTrue()
        {
            _daoMock.Setup(d => d.IsUsernameAvailable("newuser")).Returns(true);

            Assert.IsTrue(_service.IsUsernameAvailable("newuser"));
        }

        [Test]
        public void IsUsernameAvailable_Taken_ReturnsFalse()
        {
            _daoMock.Setup(d => d.IsUsernameAvailable("admin")).Returns(false);

            Assert.IsFalse(_service.IsUsernameAvailable("admin"));
        }

        [Test]
        public void IsUsernameAvailable_Exception_Throws()
        {
            _daoMock.Setup(d => d.IsUsernameAvailable(It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.IsUsernameAvailable("fail"));
        }

        [Test]
        public void IsRfcUnique_Yes_ReturnsTrue()
        {
            _daoMock.Setup(d => d.IsRfcUnique("XAXX010101000")).Returns(true);

            Assert.IsTrue(_service.IsRfcUnique("XAXX010101000"));
        }

        [Test]
        public void IsRfcUnique_Exists_ReturnsFalse()
        {
            _daoMock.Setup(d => d.IsRfcUnique("RFCEXIST")).Returns(false);

            Assert.IsFalse(_service.IsRfcUnique("RFCEXIST"));
        }

        [Test]
        public void IsRfcUnique_Exception_Throws()
        {
            _daoMock.Setup(d => d.IsRfcUnique(It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.IsRfcUnique("ERROR"));
        }

        [Test]
        public void IsPersonalEmailAvailable_Free_ReturnsTrue()
        {
            _daoMock.Setup(d => d.IsPersonalEmailAvailable("email@free.com")).Returns(true);

            Assert.IsTrue(_service.IsPersonalEmailAvailable("email@free.com"));
        }

        [Test]
        public void IsPersonalEmailAvailable_Taken_ReturnsFalse()
        {
            _daoMock.Setup(d => d.IsPersonalEmailAvailable("taken@email.com")).Returns(false);

            Assert.IsFalse(_service.IsPersonalEmailAvailable("taken@email.com"));
        }

        [Test]
        public void IsPersonalEmailAvailable_Exception_Throws()
        {
            _daoMock.Setup(d => d.IsPersonalEmailAvailable(It.IsAny<string>())).Throws(new Exception());

            Assert.Throws<Exception>(() => _service.IsPersonalEmailAvailable("fail@mail.com"));
        }
    }
}
