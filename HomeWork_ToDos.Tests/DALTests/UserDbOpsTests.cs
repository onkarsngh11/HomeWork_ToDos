using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using NUnit.Framework;
using System.Threading.Tasks;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Helpers;
using HomeWork_ToDos.CommonLib.Models.DbModels;

namespace HomeWork_ToDos.Tests.DALTests
{
    public class UserDbOpsTests : ToDoDbContextInitiator
    {
        private readonly UserDbOps _userDbOps;
        public UserDbOpsTests()
        {
            _userDbOps = new UserDbOps(DBContext, Mapper);
            DBContext.Users.Add(new UserDbModel
            {
                FirstName = "Aman",
                LastName = "Singh",
                Password = CommonHelper.EncodePasswordToBase64("123"),
                UserName = "Aman"
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Test for registering users with invalid values.
        /// </summary>
        [Test]
        public async Task Valid_RegisterUser()
        {
            bool result = await _userDbOps.RegisterUser(new CreateUserDto { FirstName = "Onkar", LastName = "Singh", Password = "123", UserName = "Onkar" });

            Assert.IsTrue(result);
        }
        /// <summary>
        /// Test for regisering user with valid values.
        /// </summary>
        [Test]
        public async Task Invalid_RegisterUser()
        {
            bool result = await _userDbOps.RegisterUser(new CreateUserDto { FirstName = "Aman", LastName = "Singh", Password = "123", UserName = "Aman" });

            Assert.IsTrue(!result);
        }
        /// <summary>
        /// Test for authentication of user.
        /// </summary>
        [Test]
        public async Task AuthenticateUser()
        {
            UserDto entity = await _userDbOps.AuthenticateUser("Aman", "123");

            Assert.NotNull(entity);
        }

    }
}