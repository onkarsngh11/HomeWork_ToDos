using HomeWork_ToDos.CommonLib.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HomeWork_ToDos.DAL.DbContexts.Configurations
{
    internal class ToDoItemEntityTypeConfiguration : IEntityTypeConfiguration<ToDoItemDbModel>
    {
        /// <summary>
        /// Configures ToDoItemDbModel
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ToDoItemDbModel> builder)
        {
            builder.ToTable("ToDoItems");
            builder.HasKey(x => x.ToDoItemId);

            builder.HasOne(t => t.ToDoLists)
                   .WithMany(t => t.ToDoItems)
                   .HasForeignKey(t => t.ToDoListId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                    new ToDoItemDbModel
                    {
                        ToDoItemId = 1,
                        Notes = "Buy IPhone 11",
                        ToDoListId = 1,
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoItemDbModel
                    {
                        ToDoItemId = 2,
                        Notes = "Buy Pixel 4a",
                        ToDoListId = 1,
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoItemDbModel
                    {
                        ToDoItemId = 3,
                        Notes = "Review Pixel 4a",
                        ToDoListId = 2,
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoItemDbModel
                    {
                        ToDoItemId = 4,
                        Notes = "Review IPhone 11",
                        ToDoListId = 2,
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    },
                    new ToDoItemDbModel
                    {
                        ToDoItemId = 5,
                        Notes = "Explore animal kingdom",
                        ToDoListId = 3,
                        CreatedBy = 1,
                        CreationDate = DateTime.Now
                    });
        }
    }
}

