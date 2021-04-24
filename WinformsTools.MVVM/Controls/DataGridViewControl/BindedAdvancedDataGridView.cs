using Zuby.ADGV;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Linq;
using WinformsTools.MVVM.Bindings;
using System.Windows.Forms;
using System;
using WinformsTools.Common.Extensions;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    /// <summary>
    /// <see href="https://github.com/davidegironi/advanceddatagridview/issues/24"/>
    /// <see href="https://github.com/OceanAirdrop/AdvancedDataGridViewDataModel"/>
    /// </summary>
    public class BindedAdvancedDataGridView : AdvancedDataGridView
    {
        public void Bind<T>(AdvancedBindingList<T> sourceList)
        {
            sourceList.FilterChanged += (sender, e) => FilterRows(sourceList.GetFiltered());

            this.RowsAdded += (sender, e) => SetRowTags(e, sourceList);
        }

        private void SetRowTags<T>(DataGridViewRowsAddedEventArgs e, AdvancedBindingList<T> sourceList)
        {
            for (var i = 0; i < e.RowCount; i++)
            {
                var rowIndex = e.RowIndex + i;
                this.Rows[rowIndex].Tag = sourceList[rowIndex].GetHashCode();
            }
        }

        private void FilterRows<T>(ICollection<T> filteredList)
        {
            var rows = this.Rows.Cast<DataGridViewRow>();
            EnforceTagUniqueness(rows);

            foreach (var row in rows)
            {
                var visible = filteredList.Any(x => x.GetHashCode() == (int)row.Tag);
                if (row.Selected && !visible)
                {
                    this.CurrentCell = null;
                }
                row.Visible = visible;
            }
        }

        private IEnumerable<DataGridViewRow> EnforceTagUniqueness(IEnumerable<DataGridViewRow> rows)
        {
            var rowsWithDuplicatedTag = rows.GroupBy(x => x.Tag).Where(x => x.Count() > 1);
            if (rowsWithDuplicatedTag.Any())
            {
                var info = rowsWithDuplicatedTag
                    .Select(x => $"[Tag: {x.Key}, Rows: {x.Select(t => t.Index).Joined()}]")
                    .Joined(";");
                throw new InvalidOperationException($"Cannot filter rows with duplicated tag ({info})");
            }

            return rows;
        }
    }
}
