using HomeWork_ToDos.BL;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ServiceTests
{
    /// <summary>
    /// User service tests.
    /// </summary>
    public class UserServiceTests
    {
        private Mock<IUserDbOps> _userDalLayer;
        private IUserContract _userService;
        readonly CreateUserDto userDto = new CreateUserDto
        {
            FirstName = "Onkar",
            LastName = "Singh",
            UserName = "Onkar",
            Password = "123"
        };

        /// <summary>
        /// Set up.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _userDalLayer = new Mock<IUserDbOps>();
            _userService = new UserService(_userDalLayer.Object);
            _userDalLayer.Setup(p => p.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new UserDto { UserId = 1 }));
            _userDalLayer.Setup(p => p.AuthenticateUser(string.Empty, string.Empty)).Returns(Task.FromResult(new UserDto { }));
            _userDalLayer.Setup(p => p.RegisterUser(userDto)).Returns(Task.FromResult(true));
        }

        /// <summary>
        /// Auth valid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_ValidUserNameAndPassword()
        {
            UserDto user = await _userService.AuthenticateUser("Onkar", "123");
            Assert.IsTrue(user.UserId == 1);
            Assert.AreEqual(1, user.UserId);
        }

        /// <summary>
        /// Auth invalid test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Authenticate_InvalidUserNameAndPassword()
        {
            UserDto user = await _userService.AuthenticateUser(string.Empty, string.Empty);
            Assert.IsTrue(user.UserId != 1);
        }
        [Test]
        public async Task RegisterUser()
        {
            bool result = await _userService.RegisterUser(userDto);
            Assert.AreEqual(true, result);
        }
    }
}
