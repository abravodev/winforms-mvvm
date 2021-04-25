using System.Windows.Forms;

namespace WinformsTools.MVVM.Controls
{
    public static class ControlExtensions
    {
        public static Control GetView(this Control control)
        {
            var view = control;

            while (view.GetType().BaseType != typeof(Form))
            {
                view = view.Parent;
            }

            return view;
        }
    }
}
