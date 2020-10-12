using HotChocolate;
using HotChocolate.Types.Relay;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using Microsoft.AspNetCore.Authorization;
using HomeWork_ToDos.CommonLib.Dtos;
using System.Collections.Generic;

namespace HomeWork_ToDos.API.GraphQl
{
    /// <summary>
    /// Query class for GraphQl.
    /// </summary>
    [Authorize(Roles = "User,Admin")]
    public class Query
    {
        private readonly ILabelDbOps _labelDbOps;
        private readonly IToDoListDbOps _toDoListDbOps;
        private readonly IToDoItemDbOps _toDoItemDbOps;
        private readonly IUserDbOps _userDbOps;
        private readonly long _userId;

        public Query([Service]ILabelDbOps labelDbOps, [Service]IToDoItemDbOps toDoItemDbOps, [Service]IToDoListDbOps toDoListDbOps,
            [Service]IUserDbOps userDbOps, IHttpContextAccessor httpContextAccessor)
        {
            _labelDbOps = labelDbOps;
            _toDoItemDbOps = toDoItemDbOps;
            _toDoListDbOps = toDoListDbOps;
            _userDbOps = userDbOps;
            if (httpContextAccessor.HttpContext.Items["UserId"] != null)
            {
                _userId = long.Parse(httpContextAccessor.HttpContext.Items["UserId"].ToString());
            }
        }

        #region Labels
        /// <summary>
        /// Get labels.
        /// </summary>
        /// <returns>Returns labels.</returns>
        public async Task<List<LabelDto>> GetAllLabels()
        {
            return await _labelDbOps.GetAllLabels(_userId);
        }

        /// <summary>
        /// Get label by id.
        /// </summary>
        /// <param name="labelId">label id.</param>
        public async Task<LabelDto> GetLabelById(long labelId)
        {
            return await _labelDbOps.GetLabelById(labelId, _userId);
        }

        #endregion

        #region Todolists

        /// <summary>
        /// Get ToDoItems.
        /// </summary>
        /// <returns>Returns ToDoItems.</returns>
        public async Task<List<ToDoItemDto>> GetAllToDoItems()
        {
            return await _toDoItemDbOps.GetAllToDoItems(_userId);
        }

        /// <summary>
        /// Get ToDoItem by id.
        /// </summary>
        /// <param name="toDoItemId">ToDoItem id.</param>        
        public async Task<ToDoItemDto> GetToDoItemById(long toDoItemId)
        {
            return await _toDoItemDbOps.GetToDoItemById(toDoItemId, _userId);
        }

        #endregion

        #region ToDoLists

        /// <summary>
        /// Get ToDoLists.
        /// </summary>
        /// <returns>Returns ToDoLists.</returns>
        public async Task<List<ToDoListDto>> GetAllToDoLists()
        {
            return await _toDoListDbOps.GetAllToDoLists(_userId);
        }

        /// <summary>
        /// Get ToDoList by id.
        /// </summary>
        /// <param name="toDoListId">ToDoList id.</param>
        public async Task<ToDoListDto> GetToDoListById(long toDoListId)
        {
            return await _toDoListDbOps.GetToDoListById(toDoListId, _userId);
        }

        #endregion

        #region Users

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <returns>Returns user details.</returns>
        public async Task<UserDto> GetById()
        {
            return await _userDbOps.GetById(_userId);
        }

        #endregion
    }
}
