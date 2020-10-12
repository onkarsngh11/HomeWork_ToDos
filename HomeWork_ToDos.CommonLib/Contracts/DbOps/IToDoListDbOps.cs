using HomeWork_ToDos.CommonLib.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.DbOps
{
    /// <summary>
    /// Contract for todolist data layer.
    /// </summary>
    public interface IToDoListDbOps
    {
        /// <summary>
        /// Gets all the ToDoList records.
        /// </summary>
        /// <param name="userId"> Id of the logged in user.</param>
        Task<List<ToDoListDto>> GetAllToDoLists(long userId);

        /// <summary>
        /// Gets ToDoList item based on Id. 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="userId"></param>
        Task<ToDoListDto> GetToDoListById(long Id, long userId);

        /// <summary>
        /// Add ToDoList item in database
        /// </summary>
        /// <param name="createToDoListDto"></param>
        Task<ToDoListDto> CreateToDoList(CreateToDoListDto createToDoListDto);

        /// <summary>
        /// Updates provided ToDoList.
        /// </summary>
        /// <param name="updateToDoListDto"></param>
        Task<ToDoListDto> UpdateToDoList(UpdateToDoListDto updateToDoListDto);

        /// <summary>
        /// Deletes ToDoList record from database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> DeleteToDoList(long id, long userId);
    }
}