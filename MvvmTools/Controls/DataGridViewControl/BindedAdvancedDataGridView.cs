using Zuby.ADGV;
using System.Linq.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace MvvmTools.Controls.DataGridViewControl
{
    /// <summary>
    /// <see href="https://github.com/davidegironi/advanceddatagridview/issues/24"/>
    /// <see href="https://github.com/OceanAirdrop/AdvancedDataGridViewDataModel"/>
    /// </summary>
    public class BindedAdvancedDataGridView : AdvancedDataGridView
    {
        private readonly FilterClause _filter = new FilterClause();
        private readonly SortClause _sort = new SortClause();

        public void Bind<T>(BindingList<T> sourceList)
        {
            ICollection<T> filteredList = sourceList;

            FilterStringChanged += (sender, e) =>
            {
                if(e.FilterString == null)
                {
                    return; // This may be caused by ourselves when updating the source list
                }

                _filter.AddFilterClause(FilterString);
                if (string.IsNullOrEmpty(FilterString))
                {
                    CleanFilter(); // For multiple filters, we cannot know which one was removed, so we remove all of them
                    DataSource = filteredList = sourceList;
                    return;
                }

                DataSource = filteredList = sourceList.Where(FilterConverter.Convert(_filter.Clause)).ToList();
            };

            SortStringChanged += (sender, e) =>
            {
                if (e.SortString == null)
                {
                    return; // This may be caused by ourselves when updating the source list
                }

                _sort.AddSortClause(SortString);
                if (string.IsNullOrEmpty(SortString))
                {
                    CleanSort(); // For multiple sorts, we cannot know which one was removed, so we remove all of them
                    DataSource = filteredList = sourceList;
                    return;
                }

                var sortStr = _sort.Clause.Replace("[", "").Replace("]", "");
                DataSource = filteredList = filteredList.OrderBy(sortStr).ToList();
            };

            sourceList.ListChanged += (sender, e) =>
            {
                TriggerFilterStringChanged();
                TriggerSortStringChanged();
            };
        }
    }
}
