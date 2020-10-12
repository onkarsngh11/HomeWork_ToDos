using HomeWork_ToDos.BL;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ServiceTests
{
    class ToDoItemServiceTests
    {
        private Mock<IToDoItemDbOps> _ToDoItemDbOps;
        private IToDoItemContract _ToDoItemContract;
        private readonly ToDoItemDto _toDoItemDto = new ToDoItemDto
        {
            ToDoItemId = 1,
            Notes = "test"
        };
        readonly List<ToDoItemDto> _toDoItemDtos = new List<ToDoItemDto>();
        readonly PaginationParameters paginationParameters = new PaginationParameters()
        {
            PageNumber = 1,
            PageSize = 10,
            SearchText = "Something"
        };
        /// <summary>
        /// Setup tests for todoitems.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _ToDoItemDbOps = new Mock<IToDoItemDbOps>();
            _ToDoItemContract = new ToDoItemService(_ToDoItemDbOps.Object);
            _ToDoItemDbOps.Setup(p => p.AddToDoItem(It.IsAny<CreateToDoItemDto>())).Returns(Task.FromResult(_toDoItemDto));
            _ToDoItemDbOps.Setup(p => p.UpdateToDoItem(It.IsAny<UpdateToDoItemDto>())).Returns(Task.FromResult(_toDoItemDto));
            _ToDoItemDbOps.Setup(p => p.DeleteToDoItem(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            _ToDoItemDbOps.Setup(p => p.GetToDoItemById(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoItemDto));
            _ToDoItemDbOps.Setup(p => p.GetAllToDoItems(It.IsAny<long>())).Returns(Task.FromResult(_toDoItemDtos));
        }

        /// <summary>
        /// Test to add ToDoItem record.
        /// </summary>
        [Test]
        public async Task AddToDoItemTest()
        {
            ToDoItemDto result = await _ToDoItemContract.AddToDoItem(new CreateToDoItemDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoItemId);
        }

        /// <summary>
        /// test to Update ToDoItem.
        /// </summary>
        [Test]
        public async Task UpdateToDoItemTest()
        {
            ToDoItemDto result = await _ToDoItemContract.UpdateToDoItem(new UpdateToDoItemDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoItemId);
        }
        /// <summary>
        /// test to delte ToDoItem record.
        /// </summary>
        [Test]
        public async Task DeleteToDoItemTest()
        {
            int result = await _ToDoItemContract.DeleteToDoItem(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }
        /// <summary>
        /// test to get ToDoItem record by id.
        /// </summary>
        [Test]
        public async Task GetToDoItemById()
        {
            ToDoItemDto result = await _ToDoItemContract.GetToDoItemById(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoItemId);
        }
        /// <summary>
        /// Test to get all todoitem records.
        /// </summary>
        [Test]
        public async Task GetToDoItems()
        {
            PagedList<ToDoItemDto> result = await _ToDoItemContract.GetToDoItems(paginationParameters, 1);
            Assert.IsNotNull(result);
        }
    }
}
