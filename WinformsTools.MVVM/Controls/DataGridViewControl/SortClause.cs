using System.Collections.Generic;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    public class SortClause
    {
        private readonly List<string> _sort = new List<string>();
        public string Clause => string.Join(",", _sort);

        public void AddSortClause(string clause)
        {
            if (string.IsNullOrEmpty(clause))
            {
                _sort.Clear();
                return;
            }

            var column = clause.Replace("ASC", "").Replace("DESC", "").Trim();
            var foundIndex = _sort.FindIndex(x => x.Contains(column));
            if (foundIndex >= 0)
            {
                _sort.RemoveAt(foundIndex);
                _sort.Insert(foundIndex, clause);
            }
            else
            {
                _sort.Add(clause);
            }
        }
    }
}
