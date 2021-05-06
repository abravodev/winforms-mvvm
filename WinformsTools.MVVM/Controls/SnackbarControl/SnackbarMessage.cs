using System;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public class SnackbarMessage : ISnackbarMessage
    {
        private static readonly TimeSpan DefaultShowTime = TimeSpan.FromSeconds(2);

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
            SnackbarProvider.GetSnackbar(_hostControl).Show(message, DefaultShowTime);
        }
    }

    public interface ISnackbarMessage
    {
        void Show(string message, TimeSpan showTime);

        void Show(string message);
    }
}
