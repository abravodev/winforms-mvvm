using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinformsTools.Common.Extensions;
using WinformsTools.Common.Helpers;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Controls.DataGridViewControl;
using WinformsTools.MVVM.Tests.TestUtils;
using Zuby.ADGV;

namespace WinformsTools.MVVM.Tests.Controls.DataGridViewControl
{
    [TestClass]
    public class BindedAdvancedDataGridViewTests
    {
        public class Data
        {
            public Data(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string FirstName { get; }

            public string LastName { get; }

            public override string ToString() => $"{FirstName} - {LastName}";
        }

        private static Data A_A = new Data("A", "A");
        private static Data A_B = new Data("A", "B");
        private static Data B_A = new Data("B", "A");
        private static Data B_B = new Data("B", "B");

        private static AdvancedDataGridView GetView(IEnumerable<Data> data)
        {
            var list = new BindingList<Data>(data.ToList());
            return GetView(list);
        }

        private static AdvancedDataGridView GetView(BindingList<Data> list)
        {
            var view = new AdvancedDataGridView();
            view.PrepareForUnitTests();
            view.AddAdvancedBinding(list);
            return view;
        }

        [TestClass]
        public class SortFeature
        {
            [TestMethod]
            public void FirstNameASC()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });

                // Act
                Sort(view, nameof(Data.FirstName), ListSortDirection.Ascending);

                // Assert
                AssertEquivalent(view, new[] { A_A, A_B, B_A, B_B });
            }

            [TestMethod]
            public void LastNameDESC()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });

                // Act
                Sort(view, nameof(Data.LastName), ListSortDirection.Descending);

                // Assert
                AssertEquivalent(view, new[] { A_B, B_B, A_A, B_A });
            }

            [TestMethod]
            public void FirstNameASC_LastNameDESC()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });

                // Act
                Sort(view, nameof(Data.FirstName), ListSortDirection.Ascending);
                Sort(view, nameof(Data.LastName), ListSortDirection.Descending);

                // Assert
                AssertEquivalent(view, new[] { A_B, A_A, B_B, B_A });
            }

            [TestMethod]
            public void FirstNameDESC_LastNameASC()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_B, B_A });

                // Act
                Sort(view, nameof(Data.FirstName), ListSortDirection.Descending);
                Sort(view, nameof(Data.LastName), ListSortDirection.Ascending);

                // Assert
                AssertEquivalent(view, new[] { B_A, B_B, A_A, A_B });
            }

            private void Sort(AdvancedDataGridView view, string headerName, ListSortDirection sortDirection)
            {
                var column = view.Columns.Cast<DataGridViewColumn>().First(x => x.HeaderText == headerName);

                if (sortDirection == ListSortDirection.Ascending)
                {
                    view.SortASC(column);
                    return;
                }

                view.SortDESC(column);
            }
        }

        [TestClass]
        public class FilterFeature
        {
            [TestMethod]
            public void FirstName()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });
                var filter = new AdvancedFilter(view);

                // Act/Assert
                filter.Filter(nameof(Data.FirstName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A, A_B });
            }

            [TestMethod]
            public void FirstName_Twice()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });
                var filter = new AdvancedFilter(view);

                // Act/Assert
                filter.Filter(nameof(Data.FirstName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A, A_B });

                filter.Filter(nameof(Data.FirstName), new[] { 'A', 'B' });
                AssertEquivalent(view, new[] { A_A, A_B, B_A, B_B });
            }

            [TestMethod]
            public void FirstName_LastName()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });
                var filter = new AdvancedFilter(view);

                // Act/Assert
                filter.Filter(nameof(Data.FirstName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A, A_B });

                filter.Filter(nameof(Data.LastName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A });
            }

            [TestMethod]
            public void ClearColumnFilter()
            {
                // Arrange
                var view = GetView(new[] { A_A, A_B, B_A, B_B });
                var filter = new AdvancedFilter(view);

                // Act/Assert
                filter.Filter(nameof(Data.FirstName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A, A_B });

                filter.Filter(nameof(Data.LastName), new[] { 'A' });
                AssertEquivalent(view, new[] { A_A });

                filter.CleanFilter(nameof(Data.FirstName));
                AssertEquivalent(view, new[] { A_A, B_A });

                filter.CleanFilter(nameof(Data.LastName));
                AssertEquivalent(view, new[] { A_A, A_B, B_A, B_B });
            }

            /// <summary>
            /// Emulates behavior of AdvancedDataGridView regarding filtering
            /// </summary>
            private class AdvancedFilter
            {
                private AdvancedDataGridView _view;
                private FilterClause _filterClause = new FilterClause();

                public AdvancedFilter(AdvancedDataGridView view)
                {
                    _view = view;
                }

                public void Filter(string columName, string[] values)
                {
                    string filter = $"[{columName}] IN ({values.Select(x => $"'{x}'").Joined()})";
                    _filterClause.TryClearColumn(columName);
                    _filterClause.AddFilterClause(filter);
                    MagicToolbox.SetProperty(_view, nameof(_view.FilterString), _filterClause.Clause);
                }

                public void Filter(string columName, char[] values)
                {
                    Filter(columName, values.Select(x => x.ToString()).ToArray());
                }

                public void CleanFilter(string headerName)
                {
                    _filterClause.ClearColumn(headerName);
                    MagicToolbox.SetProperty(_view, nameof(_view.FilterString), _filterClause.Clause);
                }
            }
        }

        private static void AssertEquivalent(AdvancedDataGridView view, IEnumerable<Data> data)
        {
            var actualRows = GetRows(view).Select(x => x.ToString()).Joined(", ");
            var expectedDataRows = data.Select(x => x.ToString()).Joined(", ");
            actualRows.Should().Be(expectedDataRows);
        }

        private static IEnumerable<Data> GetRows(AdvancedDataGridView view)
        {
            return view.Rows.Cast<DataGridViewRow>().Select(FromRow);
        }

        private static Data FromRow(DataGridViewRow row)
        {
            return new Data(
                firstName: row.Cells[0].Value.ToString(),
                lastName: row.Cells[1].Value.ToString());
        }
    }
}
