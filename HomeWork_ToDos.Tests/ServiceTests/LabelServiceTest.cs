using HomeWork_ToDos.BL;
using HomeWork_ToDos.CommonLib.Contracts.BL;
using HomeWork_ToDos.CommonLib.Contracts.DbOps;
using HomeWork_ToDos.CommonLib.Dtos;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace HomeWork_ToDos.Tests.ServiceTests
{
    /// <summary>
    /// Label service tests.
    /// </summary>
    public class LabelServiceTest
    {
        private Mock<ILabelDbOps> _labelDbOps;
        private ILabelContract _labelContract;
        private readonly LabelDto _labelDto = new LabelDto { LabelId = 1, Description = "test" };

        /// <summary>
        /// Setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _labelDbOps = new Mock<ILabelDbOps>();
            _labelContract = new LabelService(_labelDbOps.Object);
            _labelDbOps.Setup(p => p.AddLabel(It.IsAny<CreateLabelDto>())).Returns(Task.FromResult(_labelDto));
            _labelDbOps.Setup(p => p.DeleteLabel(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(1));
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddLabelTest()
        {
            LabelDto result = await _labelContract.AddLabel(new CreateLabelDto() { Description = "test", CreatedBy = 1 });
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.LabelId);
        }

        /// <summary>
        /// Add label test.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteLabelTest()
        {
            int result = await _labelContract.DeleteLabel(1, 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

    }
}
