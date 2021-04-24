using System.Collections.Generic;
using System.Linq;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    public class FilterClause
    {
        private readonly List<string> _filter = new List<string>();

        public string Clause => string.Join("AND", _filter);

        public void AddFilterClause(string clause)
        {
            if (string.IsNullOrEmpty(clause))
            {
                _filter.Clear();
                return;
            }
            _filter.Add(clause);
        }

        public void ClearColumn(string columnName)
        {
            var columnFilter = _filter.Single(x => x.Contains($"[{columnName}]"));
            _filter.Remove(columnFilter);
        }

        public void TryClearColumn(string columnName)
        {
            var columnFilter = _filter.SingleOrDefault(x => x.Contains($"[{columnName}]"));
            _filter.Remove(columnFilter);
        }
    }
}
