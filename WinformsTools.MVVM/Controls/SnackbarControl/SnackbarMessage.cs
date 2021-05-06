using System;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public class SnackbarMessage : ISnackbarMessage
    {
        private readonly Control _hostControl;

        public SnackbarMessage(Control hostControl)
        {
            _hostControl = hostControl;
        }

        public void Show(string message, TimeSpan showTime)
        {
            SnackbarProvider.GetSnackbar(_hostControl).Show(message, showTime);
        }

        public void Show(string message)
        {
            SnackbarProvider.GetSnackbar(_hostControl).Show(message);
        }
    }

    public interface ISnackbarMessage
    {
        void Show(string message, TimeSpan showTime);

        void Show(string message);
    }
}
