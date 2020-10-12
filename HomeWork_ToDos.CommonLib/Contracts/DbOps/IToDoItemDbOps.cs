using HomeWork_ToDos.CommonLib.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.DbOps
{
    /// <summary>
    /// Contract for todoItem data layer.
    /// </summary>
    public interface IToDoItemDbOps
    {
        /// <summary>
        /// Gets all the ToDoItem items in paged format based on search query.
        /// </summary>
        /// <param name="userId"> Id of the logged in user.</param>
        Task<List<ToDoItemDto>> GetAllToDoItems(long userId);

        /// <summary>
        /// Gets ToDoItem record for corresponsing ToDoListId.
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ToDoItemDto>> GetToDoItemsForToDoListId(long listId, long userId);

        /// <summary>
        /// Gets ToDoItem item based on Id. 
        /// </summary>
        /// <param name="toDoItemId"></param>
        /// <param name="userId"></param>
        Task<ToDoItemDto> GetToDoItemById(long toDoItemId, long userId);

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