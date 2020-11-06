using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;

namespace UserManager.IntegrationTests.TestUtils.Extensions
{
    public static class DataGridViewExtensions
    {
        public static IEnumerable<IDictionary<string, DataGridViewCell>> ToDictionary(this DataGridView table)
        {
            var columns = table.Header.Columns.Select(x => x.Text).ToList();
            foreach (var row in table.Rows)
            {
                yield return row.Cells.ToDictionary(columns);
            }
        }

        public static IDictionary<string, DataGridViewCell> ToDictionary(this DataGridViewCell[] cells, IEnumerable<string> columns)
        {
            var dictionary = new Dictionary<string, DataGridViewCell>();
            for (var i = 0; i < cells.Length; i++)
            {
                dictionary.Add(columns.ElementAt(i), cells[i]);
            }
            return dictionary;
        }
    }
}
