using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinformsTools.MVVM.Controls.DataGridViewControl;

namespace WinformsTools.MVVM.Tests.Controls.DataGridViewControl
{
    [TestClass]
    public class FilterConverterTests
    {
        [TestMethod]
        public void Convert_ValidSourceFilter_ReturnConvertedFilter()
        {
            // Arrange
            var filterClause = MakeFilter("[Name] IN ('john')", "[Surname] IN ('doe', 'smith')", "[AGE] IN (18, 65)");

            // Act
            var filter = FilterConverter.Convert(filterClause.Clause);

            // Assert
            filter.Should().Be(
                "(Name != null && (Name = \"john\")) AND (Surname != null && (Surname = \"doe\" OR Surname = \"smith\")) AND (AGE != null && (AGE = 18 OR AGE = 65))");
        }

        [TestMethod]
        public void Convert_StringFieldWithNumberValues_ReturnEscapedNumbers()
        {
            // Arrange
            var filterClause = MakeFilter("[Name] IN ('john', '1')", "[AGE] IN (18, 65)");

            // Act
            var filter = FilterConverter.Convert(filterClause.Clause);

            // Assert
            filter.Should().Be(
                "(Name != null && (Name = \"john\" OR Name = \"1\")) AND (AGE != null && (AGE = 18 OR AGE = 65))");
        }

        private static FilterClause MakeFilter(params string[] clauses)
        {
            var filterClause = new FilterClause();
            
            foreach (var clause in clauses)
            {
                filterClause.AddFilterClause(clause);
            }

            return filterClause;
        }
    }
}
