using HomeWork_ToDos.CommonLib.Dtos;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.DbOps
{
    public interface IUserDbOps
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Returns user id.</returns>
        Task<UserDto> AuthenticateUser(string userName, string password);

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>Returns user details.</returns>
        Task<UserDto> GetById(long userId);

        /// <summary>
        /// register users.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>BOolean result. </returns>
        Task<bool> RegisterUser(CreateUserDto userDto);
    }
}
