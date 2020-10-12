using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.BL
{
    /// <summary>
    /// Contract for todoItem service.
    /// </summary>
    public interface IToDoItemContract
    {
        /// <summary>
        /// Gets all the ToDoItem items in paged format based on search query.
        /// </summary>
        /// <param name="paginationParams">Page Size, search query and page number.</param>
        /// <param name="userId"> Id of the logged in user.</param>
        Task<PagedList<ToDoItemDto>> GetToDoItems(PaginationParameters paginationParams, long userId);

        /// <summary>
        /// Gets all the ToDoItem items in paged format based on search query.
        /// </summary>
        /// <param name="userId"> Id of the logged in user.</param>
        Task<List<ToDoItemDto>> GetAllToDoItems(long userId);

        /// <summary>
        /// gets ToDoItem records based on ToDoList id.
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        Task<List<ToDoItemDto>> GetToDoItemsForToDoListId(long listId, long userId);

        /// <summary>
        /// Gets ToDoItem record based on Id. 
        /// </summary>
        /// <param name="ToDoItemId"></param>
        /// <param name="userId"></param>
        Task<ToDoItemDto> GetToDoItemById(long ToDoItemId, long userId);

        /// <summary>
        /// Add ToDoItem item in database
        /// </summary>
        /// <param name="createToDoItemDto"></param>
        Task<ToDoItemDto> AddToDoItem(CreateToDoItemDto createToDoItemDto);

        /// <summary>
        /// Updates provided ToDoItem.
        /// </summary>
        /// <param name="updateToDoItemDto"></param>
        Task<ToDoItemDto> UpdateToDoItem(UpdateToDoItemDto updateToDoItemDto);

        /// <summary>
        /// Deletes ToDoItem record from database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteToDoItem(long id, long userId);
    }
}
