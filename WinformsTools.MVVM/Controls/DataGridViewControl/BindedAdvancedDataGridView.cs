using Zuby.ADGV;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Linq;
using WinformsTools.MVVM.Bindings;
using System.Windows.Forms;

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
        }

        private void FilterRows<T>(ICollection<T> filteredList)
        {
            foreach (var row in this.Rows.Cast<DataGridViewRow>())
            {
                var visible = filteredList.Any(x => x.GetHashCode() == row.DataBoundItem.GetHashCode());
                bool isSelected = row.Selected || row.Cells.Cast<DataGridViewCell>().Any(x => x.Selected);
                if (isSelected && !visible)
                {
                    this.CurrentCell = null;
                }
                row.Visible = visible;
            }
        }
    }
}
