using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class ToDoListEntityTypeConfiguration : IEntityTypeConfiguration<ToDoListDbModel>
    {
        /// <summary>
        /// Configures ToDoListDbModel
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ToDoListDbModel> builder)
        {
            builder.HasData(
                    new ToDoListDbModel
                    {
                        ToDoListId = 1,
                        Description = "List of phones to buy",
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoListDbModel
                    {
                        ToDoListId = 2,
                        Description = "List of phones to review",
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoListDbModel
                    {
                        ToDoListId = 3,
                        Description = "List of things to explore",
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoListDbModel
                    {
                        ToDoListId = 4,
                        Description = "List of places to travel",
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoListDbModel
                    {
                        ToDoListId = 5,
                        Description = "List of games",
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    });
        }
    }
}