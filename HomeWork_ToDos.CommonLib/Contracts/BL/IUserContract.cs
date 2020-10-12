using HomeWork_ToDos.CommonLib.Dtos;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.BL
{
    /// <summary>
    /// Contract for user service.
    /// </summary>
    public interface IUserContract
    {
        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Returns UserId.</returns>
        Task<UserDto> AuthenticateUser(string userName, string password);

        /// <summary>
        /// Get user by user id.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Returns UserDto.</returns>
        Task<UserDto> GetById(long userId);

        /// <summary>
        /// Registers user.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns> Boolean result. </returns>
        Task<bool> RegisterUser(CreateUserDto userDto);
    }
}
