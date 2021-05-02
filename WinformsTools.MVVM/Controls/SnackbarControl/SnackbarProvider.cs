using System;
using System.Linq;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public static class SnackbarProvider
    {
        /// <summary>
        /// It will get (or add) the snackbar to the main parent view of <paramref name="control"/>
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static SnackbarControl GetSnackbar(Control control)
        {
            var mainView = GetMainView(control);
            var savedSnackbar = mainView.Controls.Cast<Control>()
                .FirstOrDefault(x => x.GetType() == typeof(SnackbarControl));
            if (savedSnackbar != null)
            {
                return savedSnackbar as SnackbarControl;
            }

            var snackbar = new SnackbarControl(mainView);
            mainView.Controls.Add(snackbar);
            return snackbar;
        }

        private static Control GetMainView(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            if (control.Parent == null)
            {
                return control;
            }

            return GetMainView(control.Parent);
        }
    }
}
