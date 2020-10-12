using HomeWork_ToDos.DAL;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeWork_ToDos.CommonLib.Dtos;
using HomeWork_ToDos.CommonLib.Models.DbModels;

namespace HomeWork_ToDos.Tests.DALTests
{
    public class LabelDbOpsTests : ToDoDbContextInitiator
    {
        private readonly LabelDbOps _labelDbOps;
        public LabelDbOpsTests()
        {
            _labelDbOps = new LabelDbOps(DBContext, Mapper);
            DBContext.Labels.Add(new LabelDbModel
            {
                Description = "something",
                CreatedBy = 1,
            });
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Get labels test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetLabels()
        {
            List<LabelDto> LabelList = await _labelDbOps.GetAllLabels(1);
            int count = LabelList.Count;
            Assert.IsNotNull(LabelList);
            Assert.IsTrue(count >= 1);
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabel()
        {
            LabelDto addedLabel = await _labelDbOps.AddLabel(new CreateLabelDto { Description = "buy phone", CreatedBy = 1 });
            Assert.IsNotNull(addedLabel);
            Assert.AreEqual("buy phone", addedLabel.Description);
        }

        /// <summary>
        /// test to delete existing Label record.
        /// </summary>
        [Test]
        public async Task DeleteLabel()
        {
            int deleteResult = await _labelDbOps.DeleteLabel(1, 1);
            Assert.IsNotNull(deleteResult);
            Assert.AreEqual(1, deleteResult);
        }
    }
}