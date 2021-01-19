using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Controls.DataGridViewControl;

namespace WinformsTools.MVVM.Tests.Controls.DataGridViewControl
{
    [TestClass]
    public class FilterClauseTests
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void AddFilterClause_EmptyClause_ClearFilter(string emptyClause)
        {
            // Arrange
            var filter = new FilterClause();
            filter.AddFilterClause(clause: "anyOtherValidClause");

            // Act
            filter.AddFilterClause(emptyClause);

            // Assert
            filter.Clause.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow(new[] { "name=john" }, "name=john")]
        [DataRow(new[] { "name=john", "surname=doe" }, "name=johnANDsurname=doe")]
        public void AddFilterClause_NonEmptyClause_AddToFilter(string[] nonEmptyClause, string expectedFilter)
        {
            // Arrange
            var filter = new FilterClause();

            // Act
            foreach (var clause in nonEmptyClause)
            {
                filter.AddFilterClause(clause);
            }

            // Assert
            filter.Clause.Should().Be(expectedFilter);
        }
    }
}
