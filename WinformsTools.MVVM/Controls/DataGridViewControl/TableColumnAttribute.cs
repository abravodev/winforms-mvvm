using System;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Bindings
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TableColumnAttribute : Attribute
    {
        public DataGridViewAutoSizeColumnMode AutoSizeMode { get; set; }
    }
}
