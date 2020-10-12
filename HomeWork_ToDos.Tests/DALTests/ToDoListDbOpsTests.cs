using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.DbModels;
using HomeWork_ToDos.DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.DALTests
{
    public class ToDoListDbOpsTests : ToDoDbContextInitiator
    {
        private readonly ToDoListDbOps _toDoListDbOps;
        public ToDoListDbOpsTests()
        {
            _toDoListDbOps = new ToDoListDbOps(DBContext, Mapper);
            DBContext.ToDoLists.Add(new ToDoListDbModel
            {
                Description = "something",
                CreatedBy = 1,
                CreationDate = DateTime.Now
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetToDoLists()
        {
            List<ToDoListDto> ToDoListList = await _toDoListDbOps.GetAllToDoLists(1);
            int count = ToDoListList.Count;
            Assert.IsNotNull(ToDoListList);
            Assert.IsTrue(count >= 1);
        }

        /// <summary>
        /// Add ToDoList test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddToDoList()
        {
            ToDoListDto addedtoDoList = await _toDoListDbOps.CreateToDoList(new CreateToDoListDto { Description = "buy phone", CreatedBy = 1 });
            Assert.IsNotNull(addedtoDoList);
            Assert.AreEqual("buy phone", addedtoDoList.Description);
        }

        /// <summary>
        /// Test to update existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task UpdateToDoList()
        {
            ToDoListDto updatedToDoList = await _toDoListDbOps.UpdateToDoList(new UpdateToDoListDto { ToDoListId = 2, Description = "sell phone" });
            Assert.IsNotNull(updatedToDoList);
            Assert.AreEqual("sell phone", updatedToDoList.Description);
        }

        /// <summary>
        /// test to delete existing ToDoItem record.
        /// </summary>
        [Test]
        public async Task DeleteToDoList()
        {
            int deleteResult = await _toDoListDbOps.DeleteToDoList(1, 1);
            Assert.IsNotNull(deleteResult);
            Assert.IsTrue(deleteResult > 0);
        }
    }
}