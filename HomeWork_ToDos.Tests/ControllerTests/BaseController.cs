using HomeWork_ToDos.API.Controllers.v1;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ControllersTests
{
    /// <summary>
    /// Base class for controller tests.
    /// </summary>
    public class BaseController : MapperInitiator
    {
        public ControllerContext Context { get; }
        public ApiVersion Version { get; }

        public Mock<ILogger<LabelController>> LabelLogger { get; }
        public Mock<ILogger<ToDoItemController>> ToDoItemLogger { get; }
        public Mock<ILogger<ToDoListController>> ToDoListLogger { get; }
        public Mock<ILogger<UserController>> UserLogger { get; }

        public Mock<ILabelContract> LabelContract { get; }
        public Mock<IToDoItemContract> ToDoItemContract { get; }
        public Mock<IToDoListContract> ToDoListContract { get; }
        public Mock<IUserContract> UserContract { get; }

        private readonly ToDoItemDto _toDoItemDto = new ToDoItemDto { ToDoItemId = 1 };
        private readonly LabelDto _labelDto = new LabelDto { LabelId = 1 };
        private readonly ToDoListDto _toDoListDto = new ToDoListDto { ToDoListId = 1 };
        private readonly UserDto _userDto = new UserDto { UserId = 1, UserName = "Onkar", UserRole = "User" };

        protected BaseController()
        {
            Context = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
            Version = new ApiVersion(1, 0);
            LabelContract = new Mock<ILabelContract>();
            ToDoItemContract = new Mock<IToDoItemContract>();
            ToDoListContract = new Mock<IToDoListContract>();
            UserContract = new Mock<IUserContract>();
            LabelLogger = new Mock<ILogger<LabelController>>();
            ToDoItemLogger = new Mock<ILogger<ToDoItemController>>();
            ToDoListLogger = new Mock<ILogger<ToDoListController>>();
            UserLogger = new Mock<ILogger<UserController>>();

            Context.HttpContext.Items["UserId"] = 1;
            //Mock methods
            LabelContract.Setup(p => p.AddLabel(It.IsAny<CreateLabelDto>())).Returns(Task.FromResult(_labelDto));
            LabelContract.Setup(p => p.DeleteLabel(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            LabelContract.Setup(p => p.GetLabelById(1, 1)).Returns(Task.FromResult(_labelDto));
            ToDoItemContract.Setup(p => p.AddToDoItem(It.IsAny<CreateToDoItemDto>())).Returns(Task.FromResult(_toDoItemDto));
            ToDoItemContract.Setup(p => p.UpdateToDoItem(It.IsAny<UpdateToDoItemDto>())).Returns(Task.FromResult(_toDoItemDto));
            ToDoItemContract.Setup(p => p.DeleteToDoItem(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            ToDoItemContract.Setup(p => p.GetToDoItemById(1, 1)).Returns(Task.FromResult(_toDoItemDto));
            ToDoListContract.Setup(p => p.GetToDoListById(1, 1)).Returns(Task.FromResult(_toDoListDto));
            ToDoListContract.Setup(p => p.CreateToDoList(It.IsAny<CreateToDoListDto>())).Returns(Task.FromResult(_toDoListDto));
            ToDoListContract.Setup(p => p.UpdateToDoList(It.IsAny<UpdateToDoListDto>())).Returns(Task.FromResult(_toDoListDto));
            ToDoListContract.Setup(p => p.DeleteToDoList(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
            UserContract.Setup(p => p.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(_userDto));
            UserContract.Setup(p => p.RegisterUser(It.IsAny<CreateUserDto>())).Returns(Task.FromResult(true));
        }
    }
}
