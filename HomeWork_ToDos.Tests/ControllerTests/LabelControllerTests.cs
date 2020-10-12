using HomeWork_ToDos.API.Controllers.v1;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ControllersTests
{
    /// <summary>
    /// Label controller tests.
    /// </summary>
    public class LabelControllerTests : BaseController
    {
        private LabelController controller;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new LabelController(LabelContract.Object, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            IActionResult result = await controller.CreateLabel(new CreateLabelModel { Description = "test" }, Version);
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            IActionResult result = await controller.DeleteLabel(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabelByIdTest()
        {
            IActionResult result = await controller.GetLabelById(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
