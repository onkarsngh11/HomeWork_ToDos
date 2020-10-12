using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Helpers;
using HomeWork_ToDos.CommonLib.Models.APIModels;
using HotChocolate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace HomeWork_ToDos.API.GraphQl.Mutations
{
    /// <summary>
    /// LabelMutation class for GraphQl.
    /// </summary>
    [Authorize(Roles = "Admin,User")]
    public class Mutation
    {
        private readonly IToDoItemDbOps _toDoItemDbOps;
        private readonly IToDoListDbOps _toDoListDbOps;
        private readonly ILabelDbOps _labelDbOps;
        private readonly IUserDbOps _userDbOps;
        private readonly long _userId;
        private readonly AppSettings _appSettings;

        public Mutation([Service]ILabelDbOps labelDbOps, [Service]IToDoItemDbOps toDoItemDbOps, [Service] IToDoListDbOps toDoListDbOps,
            [Service]IUserDbOps userDbOps, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings)
        {
            _labelDbOps = labelDbOps;
            _toDoItemDbOps = toDoItemDbOps;
            _toDoListDbOps = toDoListDbOps;
            _userDbOps = userDbOps;
            _appSettings = appSettings.Value;
            if (httpContextAccessor.HttpContext.Items["UserId"] != null)
            {
                _userId = long.Parse(httpContextAccessor.HttpContext.Items["UserId"].ToString());
            }
        }

        #region Label Mutations

        /// <summary>
        /// Adds Label record
        /// </summary>
        /// <param name="createLabelDto"></param>
        /// <returns> added ToDoList record. </returns>
        public async Task<LabelDto> AddLabel(CreateLabelDto createLabelDto)
        {
            if (createLabelDto != null)
            {
                createLabelDto.CreatedBy = _userId;
            }
            LabelDto addedItem = await _labelDbOps.AddLabel(createLabelDto);
            return addedItem;
        }

        /// <summary>
        /// Delete Label record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> 1 on successful deletion else throws argument exception. </returns>
        public async Task<int> DeleteLabel(long id)
        {
            int deletedItem = await _labelDbOps.DeleteLabel(id, _userId);
            return deletedItem;
        }

        #endregion

        #region ToDoItem Mutations

        /// <summary>
        /// Adds ToDoItem record
        /// </summary>
        /// <param name="createToDoItemDto"></param>
        /// <returns> added ToDoList record. </returns>
        public async Task<ToDoItemDto> AddToDoItem(CreateToDoItemDto createToDoItemDto)
        {
            if (createToDoItemDto != null)
            {
                createToDoItemDto.CreatedBy = _userId;
            }
            ToDoItemDto addedItem = await _toDoItemDbOps.AddToDoItem(createToDoItemDto);
            return addedItem;
        }

        /// <summary>
        /// Update ToDoItem record
        /// </summary>
        /// <param name="updateToDoItemDto"></param>
        /// <returns> Updated record. </returns>
        public async Task<ToDoItemDto> UpdateToDoItem(UpdateToDoItemDto updateToDoItemDto)
        {
            ToDoItemDto updatedItem = await _toDoItemDbOps.UpdateToDoItem(updateToDoItemDto);
            return updatedItem;
        }

        /// <summary>
        /// Delete ToDoItem record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> 1 on successful deletion else throws argument exception. </returns>
        public async Task<int> DeleteToDoItem(long id)
        {
            int deletedItem = await _toDoItemDbOps.DeleteToDoItem(id, _userId);
            return deletedItem;
        }

        #endregion

        #region ToDoLItem Mutations

        /// <summary>
        /// Adds ToDoList record
        /// </summary>
        /// <param name="createToDoListDto"></param>
        /// <returns> added ToDoList record. </returns>
        public async Task<ToDoListDto> AddToDoList(CreateToDoListDto createToDoListDto)
        {
            if (createToDoListDto != null)
            {
                createToDoListDto.CreatedBy = _userId;
            }
            ToDoListDto addedItem = await _toDoListDbOps.CreateToDoList(createToDoListDto);
            return addedItem;
        }

        /// <summary>
        /// Update ToDoList record
        /// </summary>
        /// <param name="updateToDoListDto"></param>
        /// <returns> Updated record. </returns>
        public async Task<ToDoListDto> UpdateToDoList(UpdateToDoListDto updateToDoListDto)
        {
            ToDoListDto updatedItem = await _toDoListDbOps.UpdateToDoList(updateToDoListDto);
            return updatedItem;
        }

        /// <summary>
        /// Delete ToDoList record.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> 1 on successful deletion else throws argument exception. </returns>
        public async Task<int> DeleteToDoList(long id)
        {
            int deletedItem = await _toDoListDbOps.DeleteToDoList(id, _userId);
            return deletedItem;
        }
        #endregion

        #region User Mutations
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Returns user id.</returns>
        [AllowAnonymous]
        public async Task<ApiResponse<string>> AuthenticateUser(string userName, string password)
        {
            UserDto userDto = await _userDbOps.AuthenticateUser(userName, password);

            if (userDto != null)
            {
                // On Successful authentication, generate jwt token.
                string token = JwtTokenHelper.GenerateJwtToken(userDto, _appSettings);

                return new ApiResponse<string>
                {
                    IsSuccess = true,
                    Result = token,
                    Message = "Authentication successful."
                };
            }
            return null;
        }
        /// <summary>
        /// register user.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns> success/failure result.</returns>
        [AllowAnonymous]
        public async Task<bool> RegisterUser(CreateUserDto userDto)
        {
            return await _userDbOps.RegisterUser(userDto);
        }

        #endregion
    }
}
