using Zuby.ADGV;
using System.ComponentModel;
using System;

namespace WinformsTools.MVVM.Controls.DataGridViewControl
{
    /// <summary>
    /// <see href="https://github.com/davidegironi/advanceddatagridview/issues/24"/>
    /// <see href="https://github.com/OceanAirdrop/AdvancedDataGridViewDataModel"/>
    /// </summary>
    [Obsolete("You can use AdvancedDataGridView")]
    public class BindedAdvancedDataGridView : AdvancedDataGridView
    {
        public void Bind<T>(BindingList<T> sourceList)
        {
            
        }
    }
}
