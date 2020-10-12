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
    class ToDoListServiceTests
    {
        private Mock<IToDoListDbOps> _toDoListDbOps;
        private IToDoListContract _toDoListContract;
        private readonly ToDoListDto _toDoListDto = new ToDoListDto { ToDoListId = 1, Description = "test" };
        readonly List<ToDoListDto> _toDoListDtos = new List<ToDoListDto>();
        readonly PaginationParameters paginationParameters = new PaginationParameters()
        {
            PageNumber = 1,
            PageSize = 10,
            SearchText = "Something"
        };
        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _toDoListDbOps = new Mock<IToDoListDbOps>();
            _toDoListContract = new ToDoListService(_toDoListDbOps.Object);
            _toDoListDbOps.Setup(p => p.CreateToDoList(It.IsAny<CreateToDoListDto>())).Returns(Task.FromResult(_toDoListDto));
            _toDoListDbOps.Setup(p => p.UpdateToDoList(It.IsAny<UpdateToDoListDto>())).Returns(Task.FromResult(_toDoListDto));
            _toDoListDbOps.Setup(p => p.DeleteToDoList(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            _toDoListDbOps.Setup(p => p.GetToDoListById(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(_toDoListDto));
            _toDoListDbOps.Setup(p => p.GetAllToDoLists(It.IsAny<long>())).Returns(Task.FromResult(_toDoListDtos));
        }

        /// <summary>
        /// Add ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoListTest()
        {
            ToDoListDto result = await _toDoListContract.CreateToDoList(new CreateToDoListDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoListId);
        }
        [Test]
        public async Task UpdateToDoListTest()
        {
            ToDoListDto result = await _toDoListContract.UpdateToDoList(new UpdateToDoListDto());
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoListId);
        }
        [Test]
        public async Task DeleteToDoListTest()
        {
            int result = await _toDoListContract.DeleteToDoList(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }
        [Test]
        public async Task GetToDoListById()
        {
            ToDoListDto result = await _toDoListContract.GetToDoListById(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ToDoListId);
        }
        [Test]
        public async Task GetToDoLists()
        {
            PagedList<ToDoListDto> result = await _toDoListContract.GetToDoLists(paginationParameters, 1);
            Assert.IsNotNull(result);
        }
    }
}
