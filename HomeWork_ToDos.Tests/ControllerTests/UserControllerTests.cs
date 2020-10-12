using HomeWork_ToDos.CommonLib.Models.APIModels;
using HomeWork_ToDos.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ControllersTests
{
    /// <summary>
    /// User controller tests.
    /// </summary>
    public class UserControllerTests : BaseController
    {
        private UserController controller;
        private IOptions<AppSettings> options;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            options = Options.Create(new AppSettings { Secret = "this is my custom Secret key for authnetication" });
            controller = new UserController(UserLogger.Object, UserContract.Object, options, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Authentication test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AuthenticateTest()
        {
            IActionResult result = await controller.Login(new LoginModel { UserName = "Onkar", Password = "123" });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }


        /// <summary>
        /// Authentication test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task RegistrationTest()
        {
            IActionResult result = await controller.Register(new CreateUserModel { FirstName = "Onkar", LastName = "Singh", UserName = "Onkar", Password = "123" });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

    }
}
