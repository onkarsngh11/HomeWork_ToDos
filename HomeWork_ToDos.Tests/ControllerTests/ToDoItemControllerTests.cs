using HomeWork_ToDos.API.Controllers.v1;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ControllersTests
{
    /// <summary>
    /// Items controller tests.
    /// </summary>
    public class ToDoItemControllerTests : BaseController
    {
        private ToDoItemController controller;

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            controller = new ToDoItemController(ToDoItemContract.Object, Mapper)
            {
                ControllerContext = Context
            };
        }

        /// <summary>
        /// Add item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddItemTest()
        {
            IActionResult result = await controller.CreateToDoItem(new CreateToDoItemModel { Notes = "test", ToDoListId = 1 }, Version);
            CreatedAtActionResult response = result as CreatedAtActionResult;
            Assert.AreEqual(StatusCodes.Status201Created, (int)response.StatusCode);
        }

        /// <summary>
        /// Update item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateItemTest()
        {
            IActionResult result = await controller.PutToDoItem(new UpdateToDoItemModel { ToDoItemId = 1, Notes = "test" });
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Delete item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            IActionResult result = await controller.DeleteToDoItem(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }

        /// <summary>
        /// Get item test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetItemTest()
        {
            IActionResult result = await controller.GetToDoItemById(1);
            OkObjectResult response = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, (int)response.StatusCode);
        }
    }
}
