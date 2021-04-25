using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinformsTools.MVVM.Controls;

namespace WinformsTools.MVVM.Telemetry
{
    public class ClickMetric : IMetric
    {
        private Control _control;

        public ClickMetric(Control control)
        {
            _control = control;
        }

        public string MessageTemplate => "Control '{Control}' was clicked";

        public object[] MessageParams => new object[] { _control.Name };

        public IDictionary<string, object> Context => CreateContext(("View", _control.GetView().Name));

        private static IDictionary<string, object> CreateContext(params (string PropertyName, object Value)[] context)
        {
            return context.ToDictionary(x => x.PropertyName, x => x.Value);
        }
    }
}
