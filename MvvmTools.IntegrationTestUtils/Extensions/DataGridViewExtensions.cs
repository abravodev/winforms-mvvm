using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using System.Collections.Generic;
using System.Linq;

namespace MvvmTools.IntegrationTestUtils.Extensions
{
    public static class DataGridViewExtensions
    {
        public static DataGridViewRow[] GetRows(this DataGridView table)
        {
            return Retry.WhileEmpty(() => table.Rows).Result;
        }

        public static IEnumerable<IDictionary<string, DataGridViewCell>> ToDictionary(this DataGridView table)
        {
            var columns = table.Header.Columns.Select(x => x.Text).ToList();
            foreach (var row in table.Rows)
            {
                yield return row.Cells.ToDictionary(columns);
            }
        }

        public static IEnumerable<IDictionary<string, DataGridViewCell>> ToDictionary(this DataGridViewRow[] rows)
        {
            var table = rows[0].Parent.AsDataGridView();
            var columns = table.Header.Columns.Select(x => x.Text).ToList();
            foreach (var row in rows)
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
