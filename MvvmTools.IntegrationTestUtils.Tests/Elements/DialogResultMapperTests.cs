using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using MvvmTools.IntegrationTestUtils.Elements;

namespace MvvmTools.IntegrationTestUtils.Tests.Elements
{
    [TestClass]
    public class DialogResultMapperTests
    {
        [TestMethod]
        [DataRow(1, "2", DialogResult.OK)]
        [DataRow(2, "1", DialogResult.OK)]
        [DataRow(2, "2", DialogResult.Cancel)]
        [DataRow(1, "3", DialogResult.Abort)]
        [DataRow(1, "4", DialogResult.Retry)]
        [DataRow(1, "5", DialogResult.Ignore)]
        [DataRow(1, "6", DialogResult.Yes)]
        [DataRow(1, "7", DialogResult.No)]
        public void ToAutomationId(int numberOfOptions, string expected, DialogResult result)
        {
            var automationId = DialogResultMapper.ToAutomationId(numberOfOptions, result);
            automationId.Should().Be(expected);
        }

        [TestMethod]
        [DataRow(1, "2", DialogResult.OK)]
        [DataRow(2, "1", DialogResult.OK)]
        [DataRow(2, "2", DialogResult.Cancel)]
        [DataRow(1, "3", DialogResult.Abort)]
        [DataRow(1, "4", DialogResult.Retry)]
        [DataRow(1, "5", DialogResult.Ignore)]
        [DataRow(1, "6", DialogResult.Yes)]
        [DataRow(1, "7", DialogResult.No)]
        public void FromAutomationId(int numberOfOptions, string automationId, DialogResult expected)
        {
            var result = DialogResultMapper.FromAutomationId(numberOfOptions, automationId);
            result.Should().Be(expected);
        }
    }
}
