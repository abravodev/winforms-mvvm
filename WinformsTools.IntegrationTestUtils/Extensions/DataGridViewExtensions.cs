using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using System.Collections.Generic;
using System.Linq;

namespace WinformsTools.IntegrationTestUtils.Extensions
{
    public static class DataGridViewExtensions
    {
        public static DataGridViewRow[] GetRows(this DataGridView table)
        {
            return Retry.WhileEmpty(() => GetDataGridViewRow(table)).Result;
        }

        public static IEnumerable<IDictionary<string, DataGridViewCell>> ToDictionary(this DataGridView table)
        {
            var columns = GetCellNames(table);
            foreach (var row in table.Rows)
            {
                yield return row.Cells.ToDictionary(columns);
            }
        }

        public static IEnumerable<IDictionary<string, DataGridViewCell>> ToDictionary(this DataGridViewRow[] rows)
        {
            var table = rows[0].Parent.AsDataGridView();
            var columns = GetCellNames(table);
            foreach (var row in rows)
            {
                yield return row.Cells.ToDictionary(columns);
            }
        }

        private static List<string> GetCellNames(DataGridView table)
        {
            var header = GetDataGridViewHeader(table);
            var headerColumns = GetDataGridViewHeaderColumns(header);
            return headerColumns.Select(x => x.Text).ToList();
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

        /// <summary>
        /// <see cref="DataGridView.Rows"/> does not work in culture different than English or German:
        /// <see href="https://github.com/FlaUI/FlaUI/blob/master/src/FlaUI.Core/AutomationElements/DataGridView.cs"/>
        /// <see href="https://github.com/FlaUI/FlaUI/blob/master/src/FlaUI.Core/Tools/LocalizedStrings.cs"/>
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static DataGridViewRow[] GetDataGridViewRow(DataGridView dataGridView)
        {
            var rows = dataGridView.FindAllChildren(cf => 
                cf.ByControlType(ControlType.Custom)
                    .Or(cf.ByControlType(ControlType.DataItem))
                .And(cf.ByName(CustomLocalizedStrings.DataGridViewHeader).Not())
            );
            // Remove the last row if we have the "add" row
            if (dataGridView.HasAddRow)
            {
                rows = rows.Take(rows.Length - 1).ToArray();
            }
            return rows.Select(x => new DataGridViewRow(x.FrameworkAutomationElement)).ToArray();
        }

        /// <summary>
        /// <see cref="DataGridView.Header"/> does not work in culture different than English or German
        /// <see href="https://github.com/FlaUI/FlaUI/blob/master/src/FlaUI.Core/AutomationElements/DataGridView.cs"/>
        /// <see href="https://github.com/FlaUI/FlaUI/blob/master/src/FlaUI.Core/Tools/LocalizedStrings.cs"/>
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static DataGridViewHeader GetDataGridViewHeader(DataGridView dataGridView)
        {
            var header = dataGridView.FindFirstChild(cf => 
                cf.ByName(CustomLocalizedStrings.DataGridViewHeader)
                    .Or(cf.ByControlType(ControlType.Header)));
            return header == null ? null : new DataGridViewHeader(header.FrameworkAutomationElement);
        }

        /// <summary>
        /// <see cref="DataGridViewHeader.Columns"/> does not work in culture different than English or German
        /// </summary>
        /// <returns></returns>
        private static DataGridViewHeaderItem[] GetDataGridViewHeaderColumns(DataGridViewHeader dataGridViewHeader)
        {
            // WinForms uses Header control type, WPF uses HeaderItem control type
            var headerItems = dataGridViewHeader.FindAllChildren(cf => 
                cf.ByControlType(ControlType.Header)
                    .Or(cf.ByControlType(ControlType.HeaderItem)));
            var convertedHeaderItems = headerItems.Select(x => new DataGridViewHeaderItem(x.FrameworkAutomationElement))
                .ToList();
            // Remove the top-left header item if it exists (can be the first or last item)
            if (convertedHeaderItems.Last().Text == CustomLocalizedStrings.DataGridViewHeaderItemTopLeft)
            {
                convertedHeaderItems = convertedHeaderItems.Take(convertedHeaderItems.Count - 1).ToList();
            }
            else if (convertedHeaderItems.First().Text == CustomLocalizedStrings.DataGridViewHeaderItemTopLeft)
            {
                convertedHeaderItems = convertedHeaderItems.Skip(1).ToList();
            }
            return convertedHeaderItems.ToArray();
        }
    }
}
