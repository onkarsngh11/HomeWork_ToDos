using HomeWork_ToDos.CommonLib.Helpers;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserDbModel>
    {
        /// <summary>
        /// Inserts initial user records since other tables have dependency on it.
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserDbModel> builder)
        {
            builder.HasData(new UserDbModel
            {
                UserId = 1,
                FirstName = "Onkar",
                LastName = "Singh",
                UserName = "Onkar",
                Password = CommonHelper.EncodePasswordToBase64("123"),
                UserRole = "Admin"
            });
        }
    }
}
