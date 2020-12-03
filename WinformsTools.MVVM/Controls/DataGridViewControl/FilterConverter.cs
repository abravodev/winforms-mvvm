using System;
using System.Text;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    public static class FilterConverter
    {
        public static string Convert(string sourceFilter)
        {
            var filter = new StringBuilder();

            var columnFilters = sourceFilter
                .Replace("(", "").Replace(")", "") // get rid of all the parenthesis
                .Split(new string[] { "AND" }, StringSplitOptions.None); // now split the string on the 'and' (each grid column)

            string andOperator = "";

            foreach (var columnFilter in columnFilters)
            {
                filter.Append(andOperator);

                // split string on the 'in'
                var columnFilterData = columnFilter.Trim().Split(new string[] { "IN" }, StringSplitOptions.None);

                // get string between square brackets
                var colName = columnFilterData[0].Split('[', ']')[1].Trim();

                // prepare beginning of linq statement
                filter.Append(string.Format("({0} != null && (", colName));

                string orOperator = "";

                var filterValues = columnFilterData[1].Split(',');

                foreach (var filterValue in filterValues)
                {
                    // remove any single quotes before testing if filter is a num or not
                    var cleanFilterVal = filterValue.Replace("'", "").Trim();

                    double tempNum = 0;
                    if (double.TryParse(cleanFilterVal, out tempNum))
                        filter.Append(string.Format("{0} {1} = {2}", orOperator, colName, cleanFilterVal.Trim()));
                    else
                        filter.Append(string.Format("{0} {1} = '{2}'", orOperator, colName, cleanFilterVal.Trim()));

                    orOperator = " OR ";
                }

                filter.Append("))");

                andOperator = " AND ";
            }

            // replace all single quotes with double quotes
            return filter.ToString().Replace("'", "\"");
        }
    }
}
