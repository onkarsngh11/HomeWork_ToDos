using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.BL
{
    /// <summary>
    /// Implemenation of ITodoListService contract.
    /// </summary>
    public class ToDoListService : IToDoListContract
    {
        private readonly IToDoListDbOps _toDoListDbOps;
        public ToDoListService(IToDoListDbOps toDoListDbOps)
        {
            _toDoListDbOps = toDoListDbOps;
        }
        /// <summary>
        /// Get ToDoList record By Id 
        /// </summary>
        /// <param name="toDoListId"></param>
        /// <param name="userId"></param>
        /// <returns>ToDoList record based on ToDoListId</returns>
        public async Task<ToDoListDto> GetToDoListById(long toDoListId, long userId)
        {
            return await _toDoListDbOps.GetToDoListById(toDoListId, userId);
        }

        /// <summary>
        /// Get all ToDoLists created by user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Returns all ToDolLsts created by user.</returns>
        public async Task<List<ToDoListDto>> GetAllToDoLists(long userId)
        {
            return await _toDoListDbOps.GetAllToDoLists(userId);
        }

        /// <summary>
        /// returns all the to do lists in paged format based on search query for the logged in user.
        /// </summary>
        /// <param name="paginationParams">Page Size, search query and page number.</param>
        /// <param name="userId"> Id of the logged in user.</param>
        /// <returns>Return paged lists based on input params</returns>
        public async Task<PagedList<ToDoListDto>> GetToDoLists(PaginationParameters paginationParams, long userId)
        {
            List<ToDoListDto> todoLists = await _toDoListDbOps.GetAllToDoLists(userId);
            if (!string.IsNullOrWhiteSpace(paginationParams.SearchText))
            {
                todoLists = todoLists.Where(p => p.Description.Contains(paginationParams.SearchText)).ToList();
            }
            return PagedList<ToDoListDto>.ToPagedList(todoLists, paginationParams.PageNumber, paginationParams.PageSize);
        }

        /// <summary>
        /// Create ToDoList record.
        /// </summary>
        /// <param name="createToDoItemDto">Description,CreationDate,CreatedBy</param>
        /// <returns> Created ToDoItem record. </returns>
        public async Task<ToDoListDto> CreateToDoList(CreateToDoListDto createToDoListDto)
        {
            return await _toDoListDbOps.CreateToDoList(createToDoListDto);
        }

        /// <summary>
        /// Update specified ToDoList record.
        /// </summary>
        /// <param name="updateToDoListDto">Description,UpdationDate,ToDoListId</param>
        /// <returns> Updated ToDoList record ,</returns>
        public async Task<ToDoListDto> UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            return await _toDoListDbOps.UpdateToDoList(updateToDoListDto);
        }

        /// <summary>
        /// Delete specified ToDoList record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns> 1 on success, 0 on failure. </returns>
        public async Task<int> DeleteToDoList(long id, long userId)
        {
            return await _toDoListDbOps.DeleteToDoList(id, userId);
        }
    }
}
