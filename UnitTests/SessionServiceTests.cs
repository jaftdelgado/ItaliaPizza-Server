using NUnit.Framework;
using Moq;
using Services;
using Services.Dtos;
using System;

namespace UnitTests
{
    public class SessionServiceTests
    {
        private Mock<ISessionDAO> _mockDAO;
        private SessionService _service;

        [SetUp]
        public void Setup()
        {
            _mockDAO = new Mock<ISessionDAO>();
            _service = new SessionService(_mockDAO.Object);
        }

        [Test]
        public void SignIn_WithValidCredentials_ReturnsPersonalDTO()
        {
            var expected = new PersonalDTO { Username = "admin" };
            _mockDAO.Setup(d => d.SignIn("admin", "1234")).Returns(expected);

            var result = _service.SignIn("admin", "1234");

            Assert.IsNotNull(result);
            Assert.That(result.Username, Is.EqualTo("admin"));
        }

        [Test]
        public void SignIn_WithInvalidCredentials_ReturnsNull()
        {
            _mockDAO.Setup(d => d.SignIn("wrong", "wrong")).Returns((PersonalDTO)null);

            var result = _service.SignIn("wrong", "wrong");

            Assert.IsNull(result);
        }

        [Test]
        public void SignIn_WhenExceptionThrown_Throws()
        {
            _mockDAO.Setup(d => d.SignIn(It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.SignIn("user", "pass"));
        }

        [Test]
        public void UpdateActivity_WithValidId_ReturnsOne()
        {
            _mockDAO.Setup(d => d.UpdateActivity(1)).Returns(1);

            var result = _service.UpdateActivity(1);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void UpdateActivity_WithInvalidId_ReturnsZero()
        {
            _mockDAO.Setup(d => d.UpdateActivity(999)).Returns(0);

            var result = _service.UpdateActivity(999);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void UpdateActivity_WhenExceptionThrown_Throws()
        {
            _mockDAO.Setup(d => d.UpdateActivity(It.IsAny<int>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.UpdateActivity(1));
        }

        [Test]
        public void SignOut_WithValidId_ReturnsOne()
        {
            _mockDAO.Setup(d => d.SignOut(1)).Returns(1);

            var result = _service.SignOut(1);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void SignOut_WithInvalidId_ReturnsZero()
        {
            _mockDAO.Setup(d => d.SignOut(999)).Returns(0);

            var result = _service.SignOut(999);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SignOut_WhenExceptionThrown_Throws()
        {
            _mockDAO.Setup(d => d.SignOut(It.IsAny<int>())).Throws<Exception>();

            Assert.Throws<Exception>(() => _service.SignOut(1));
        }
    }
}
