using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Controls.DataGridViewControl;

namespace WinformsTools.MVVM.Tests.Controls.DataGridViewControl
{
    [TestClass]
    public class SortClauseTests
    {
        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void AddSortClause_EmptyClause_ClearSort(string emptyClause)
        {
            // Arrange
            var sort = new SortClause();
            sort.AddSortClause(clause: "anyOtherValidClause");

            // Act
            sort.AddSortClause(emptyClause);

            // Assert
            sort.Clause.Should().BeEmpty();
        }

        [DataTestMethod]
        [DataRow(new[] { "name" }, "name")]
        [DataRow(new[] { "name" }, "name")]
        [DataRow(new[] { "name", "surname" }, "name,surname")]
        [DataRow(new[] { "nameASC", "surname", "nameDESC" }, "nameDESC,surname")]
        public void AddSortClause_NonEmptyClause_AddToSort(string[] nonEmptyClause, string expectedSort)
        {
            // Arrange
            var sort = new SortClause();

            // Act
            foreach (var clause in nonEmptyClause)
            {
                sort.AddSortClause(clause);
            }

            // Assert
            sort.Clause.Should().Be(expectedSort);
        }
    }
}
