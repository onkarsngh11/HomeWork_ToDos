using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.DALTests
{
    public class ToDoItemDbOpsTests : ToDoDbContextInitiator
    {
        private readonly ToDoItemDbOps _toDoItemDbOps;
        public ToDoItemDbOpsTests()
        {
            _toDoItemDbOps = new ToDoItemDbOps(DBContext, Mapper);
            DBContext.ToDoItems.Add(new ToDoItemDbModel
            {
                Notes = "something",
                CreatedBy = 1,
                ToDoListId = 1,
                CreationDate = DateTime.Now
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get ToDoItems test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoItems()
        {
            List<ToDoItemDto> toDoItemList = await _toDoItemDbOps.GetAllToDoItems(1);
            int count = toDoItemList.Count;
            Assert.IsNotNull(toDoItemList);
            Assert.IsTrue(count >= 1);
        }

        /// <summary>
        /// Add ToDoItem test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoItem()
        {
            ToDoItemDto addedToDoItem = await _toDoItemDbOps.AddToDoItem(new CreateToDoItemDto { Notes = "buy phone", CreatedBy = 1, ToDoListId = 1 });
            Assert.IsNotNull(addedToDoItem);
            Assert.AreEqual("buy phone", addedToDoItem.Notes);
        }

        /// <summary>
        /// Test to update existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task UpdateToDoItem()
        {
            ToDoItemDto updatedToDoItem = await _toDoItemDbOps.UpdateToDoItem(new UpdateToDoItemDto { ToDoItemId = 2, Notes = "sell phone" });
            Assert.IsNotNull(updatedToDoItem);
            Assert.AreEqual("sell phone", updatedToDoItem.Notes);
        }

        /// <summary>
        /// test to delete existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task DeleteToDoItem()
        {
            int deleteResult = await _toDoItemDbOps.DeleteToDoItem(1, 1);
            Assert.IsNotNull(deleteResult);
            Assert.IsTrue(deleteResult > 0);
        }
    }
}