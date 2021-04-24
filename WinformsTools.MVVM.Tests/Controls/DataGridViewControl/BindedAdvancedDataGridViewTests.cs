using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using WinformsTools.Common.Extensions;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Controls.DataGridViewControl;
using WinformsTools.MVVM.Tests.TestUtils;

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

        private static BindedAdvancedDataGridView GetView(Data[] data)
        {
            var list = new AdvancedBindingList<Data>(data.ToList());
            var view = new BindedAdvancedDataGridView();
            view.PrepareForUnitTests();
            view.Bind(list);
            view.AddBinding(list);

            return view;
        }

        [TestMethod]
        public void Sort_FirstNameASC()
        {
            // Arrange
            var view = GetView(new[] { A_A, A_B, B_A, B_B });

            // Act
            Sort(view, nameof(Data.FirstName), ListSortDirection.Ascending);

            // Assert
            AssertEquivalent(view, new[] { A_A, A_B, B_A, B_B });
        }

        [TestMethod]
        public void Sort_LastNameDesc()
        {
            // Arrange
            var view = GetView(new[] { A_A, A_B, B_A, B_B });

            // Act
            Sort(view, nameof(Data.LastName), ListSortDirection.Descending);

            // Assert
            AssertEquivalent(view, new[] { A_B, B_B, A_A, B_A });
        }

        [TestMethod]
        public void Sort_FirstNameASCAndLastNameDESC()
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
        public void Sort_FirstNameDESCAndLastNameASC()
        {
            // Arrange
            var view = GetView(new[] { A_A, A_B, B_B, B_A });

            // Act
            Sort(view, nameof(Data.FirstName), ListSortDirection.Descending);
            Sort(view, nameof(Data.LastName), ListSortDirection.Ascending);

            // Assert
            AssertEquivalent(view, new[] { B_A, B_B, A_A, A_B });
        }

        private void Sort(BindedAdvancedDataGridView view, string headerName, ListSortDirection sortDirection)
        {
            var column = view.Columns.Cast<DataGridViewColumn>().First(x => x.HeaderText == headerName);

            if (sortDirection == ListSortDirection.Ascending)
            {
                view.SortASC(column);
                return;
            }

            view.SortDESC(column);
        }

        private void AssertEquivalent(BindedAdvancedDataGridView view, IEnumerable<Data> data)
        {
            var actualRows = GetRows(view).Select(x => x.ToString()).Joined(", ");
            var expectedDataRows = data.Select(x => x.ToString()).Joined(", ");
            actualRows.Should().Be(expectedDataRows);
        }

        private IEnumerable<Data> GetRows(BindedAdvancedDataGridView view)
        {
            return view.Rows.Cast<DataGridViewRow>().Select(FromRow);
        }

        private Data FromRow(DataGridViewRow row)
        {
            return new Data(
                firstName: row.Cells[0].Value.ToString(),
                lastName: row.Cells[1].Value.ToString());
        }
    }
}
