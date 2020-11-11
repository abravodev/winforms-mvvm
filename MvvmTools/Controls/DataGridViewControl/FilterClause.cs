using System.Collections.Generic;

namespace MvvmTools.Controls.DataGridViewControl
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
    }
}
