using System;
using System.Collections.Generic;
using System.Linq;
using WinformsTools.Common.Extensions;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    public static class FilterConverter
    {
        public static string Convert(string sourceFilter)
        {
            var columnFilters = sourceFilter
                .Replace("(", "").Replace(")", "") // get rid of all the parenthesis
                .Split(new string[] { "AND" }, StringSplitOptions.None); // now split the string on the 'and' (each grid column)

            var filter = columnFilters.Select(GetColumnFilter).Joined(" AND ");

            // replace all single quotes with double quotes
            return filter.Replace("'", "\"");
        }

        private static string GetColumnFilter(string columnFilter)
        {
            var (columName, filterValues) = GetFilter(columnFilter);
            var posibleValues = filterValues.Select(filterValue =>
            {
                return IsNumber(filterValue)
                    ? $"{columName} = {filterValue}"
                    : $"{columName} = '{filterValue}'";
            }).Joined(" OR ");

            return $"({columName} != null && ({posibleValues}))";
        }

        private static bool IsNumber(string value) => double.TryParse(value, out double _);

        private static (string Name, List<string> Values) GetFilter(string columnFilter)
        {
            // split string on the 'in'
            var columnFilterData = columnFilter.Trim().Split(new string[] { "IN" }, StringSplitOptions.None);

            // get string between square brackets
            var columName = columnFilterData[0].Split('[', ']')[1].Trim();

            // remove any single quotes before testing if filter is a num or not
            var filterValues = columnFilterData[1].Split(',')
                .Select(x => x.Replace("'", "").Trim())
                .ToList();
            
            return (columName, filterValues);
        }
    }
}
