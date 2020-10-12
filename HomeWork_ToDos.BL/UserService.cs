using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using System.Threading.Tasks;

namespace HomeWork_ToDos.BL
{
    /// <summary>
    /// This class implements Buisness level logic and calls Data access layer for DB Operations.
    /// </summary>
    public class UserService : IUserContract
    {
        private readonly IUserDbOps _userDbOps;

        public UserService(IUserDbOps userDbOps)
        {
            _userDbOps = userDbOps;
        }

        /// <summary>
        /// Returns UserId on successful authentication else returns null.
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        /// <returns>Returns UserId.</returns>
        public async Task<UserDto> AuthenticateUser(string userName, string password)
        {
            return await _userDbOps.AuthenticateUser(userName, password);
        }

        /// <summary>
        /// Returns userId of user if found else returns null.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Returns UserDto.</returns>
        public async Task<UserDto> GetById(long userId)
        {
            return await _userDbOps.GetById(userId);
        }

        /// <summary>
        /// Registers User. 
        /// </summary>
        /// <param name="userDto">FirstName,LastName,UserName,Password</param>
        /// <returns>true on registration success else false. </returns>
        public async Task<bool> RegisterUser(CreateUserDto userDto)
        {
            return await _userDbOps.RegisterUser(userDto);
        }
    }
}