using AutoMapper;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Helpers;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork_ToDos.CommonLib.Contracts.DbOps
{
    /// <summary>
    /// Performs database operations related to User Object.
    /// </summary>
    public class UserDbOps : IUserDbOps
    {
        private readonly ToDoDbContext _toDoDbContext;

        private readonly IMapper _mapper;

        public UserDbOps(ToDoDbContext toDoDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _toDoDbContext = toDoDbContext;
        }

        /// <summary>
        /// Authenticate user.
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">password</param>
        /// <returns>Returns UserId</returns>
        public async Task<UserDto> AuthenticateUser(string userName, string password)
        {
            password = CommonHelper.EncodePasswordToBase64(password);
            UserDbModel user = await _toDoDbContext.Users
                .Where(p => p.UserName.ToLower() == userName.ToLower() && p.Password == password).FirstOrDefaultAsync();
            if (null == user)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Get specific user.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Returns UserDto</returns>
        public async Task<UserDto> GetById(long userId)
        {
            UserDbModel user = await _toDoDbContext.Users.Where(p => p.UserId == userId).SingleOrDefaultAsync();
            if (null == user)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns> Success/Failure result</returns>
        public async Task<bool> RegisterUser(CreateUserDto userDto)
        {
            if (userDto.Password != null)
            {
                userDto.Password = CommonHelper.EncodePasswordToBase64(userDto.Password);
            }

            UserDbModel userName = await _toDoDbContext.Users
                .Where(p => p.UserName.ToLower() == userDto.UserName.ToLower()).FirstOrDefaultAsync();

            if (userName != null)           //This will prevent addition of existing username again
            {
                return false;
            }
            UserDbModel user = _mapper.Map<UserDbModel>(userDto);
            if (user.UserRole == null)
            {
                user.UserRole = "User";
            }
            _toDoDbContext.Users.Add(user);
            if (await _toDoDbContext.SaveChangesAsync() == 1)
            {
                return true;
            }
            return false;
        }
    }
}
