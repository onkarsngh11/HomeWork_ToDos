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
    /// Implemenation of ITodoItemService contract.
    /// </summary>
    public class ToDoItemService : IToDoItemContract
    {
        private readonly IToDoItemDbOps _toDoItemDbOps;
        public ToDoItemService(IToDoItemDbOps toDoItemDbOps)
        {
            _toDoItemDbOps = toDoItemDbOps;
        }
        /// <summary>
        /// Get ToDoItem record By Id 
        /// </summary>
        /// <param name="ToDoItemId"></param>
        /// <param name="userId"></param>
        /// <returns>ToDoItem record based on ToDoItemId</returns>
        public async Task<ToDoItemDto> GetToDoItemById(long ToDoItemId, long userId)
        {
            return await _toDoItemDbOps.GetToDoItemById(ToDoItemId, userId);
        }

        /// <summary>
        /// Get all ToDoItems created by user.
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <returns>Returns all ToDoItems created by user.</returns>
        public async Task<List<ToDoItemDto>> GetToDoItemsForToDoListId(long listId, long userId)
        {
            return await _toDoItemDbOps.GetToDoItemsForToDoListId(listId, userId);
        }

        /// <summary>
        /// Get all ToDoItems created by user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Returns all ToDoItems created by user.</returns>
        public async Task<List<ToDoItemDto>> GetAllToDoItems(long userId)
        {
            return await _toDoItemDbOps.GetAllToDoItems(userId);
        }

        /// <summary>
        /// returns all the to do Items in paged format based on search query for the logged in user.
        /// </summary>
        /// <param name="paginationParams"></param>
        /// <param name="userId"></param>
        /// <returns> Pagedlst of ToDoItem records. </returns>
        public async Task<PagedList<ToDoItemDto>> GetToDoItems(PaginationParameters paginationParams, long userId)
        {
            List<ToDoItemDto> todoItems = await _toDoItemDbOps.GetAllToDoItems(userId);
            if (!string.IsNullOrWhiteSpace(paginationParams.SearchText))
            {
                todoItems = todoItems.Where(p => p.Notes.Contains(paginationParams.SearchText)).ToList();
            }
            return PagedList<ToDoItemDto>.ToPagedList(todoItems, paginationParams.PageNumber, paginationParams.PageSize);
        }

        /// <summary>
        /// Create ToDoItem record.
        /// </summary>
        /// <param name="createToDoItemDto"></param>
        /// <returns> Created ToDoItem record. </returns>
        public async Task<ToDoItemDto> AddToDoItem(CreateToDoItemDto createToDoItemDto)
        {
            return await _toDoItemDbOps.AddToDoItem(createToDoItemDto);
        }

        /// <summary>
        /// Updates specified ToDoItem record.
        /// </summary>
        /// <param name="updateToDoItemDto"></param>
        /// <returns> Updated ToDoItem record. </returns>
        public async Task<ToDoItemDto> UpdateToDoItem(UpdateToDoItemDto updateToDoItemDto)
        {
            return await _toDoItemDbOps.UpdateToDoItem(updateToDoItemDto);
        }

        /// <summary>
        /// Delete ToDoItem record.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns> 1 on success, 0 on failure. </returns>
        public async Task<int> DeleteToDoItem(long id, long userId)
        {
            return await _toDoItemDbOps.DeleteToDoItem(id, userId);
        }
    }
}
