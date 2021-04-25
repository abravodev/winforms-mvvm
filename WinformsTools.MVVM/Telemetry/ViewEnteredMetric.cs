using System.Collections.Generic;
using System.Windows.Forms;
using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Telemetry
{
    public class ViewEnteredMetric<TViewModel> : IMetric where TViewModel : IViewModel
    {
        private IView<TViewModel> _view;

        public ViewEnteredMetric(IView<TViewModel> view)
        {
            _view = view;
        }

        public string MessageTemplate => "User entered view '{ViewName}'";

        public object[] MessageParams => new object[] { GetViewName() };

        private string GetViewName()
        {
            var form = (Control)_view;
            return form.Name;
        }

        public IDictionary<string, object> Context => new Dictionary<string, object>();
    }
}
