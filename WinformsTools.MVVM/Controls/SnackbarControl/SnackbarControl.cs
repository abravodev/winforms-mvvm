using System;
using System.Windows.Forms;
using WinformsTools.MVVM.Components;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public partial class SnackbarControl : UserControl
    {
        private readonly Control _host;
        private System.Timers.Timer _timer;

        public SnackbarControl(Control host)
        {
            _host = host;
            InitializeComponent();
            Setup();
        }

        public SnackbarControl()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Hide();
            this.Dock = DockStyle.Bottom;
        }

        public void Show(string message) => Show(message, TimeSpan.FromSeconds(5));

        public void Show(string message, TimeSpan showTime)
        {
            lbl_snackbarMessage.Text = message;
            this.BringToFront();
            Show();
            SetTimeout(showTime, Hide);
        }

        private void SetTimeout(TimeSpan delay, Action action)
        {
            _timer?.Stop();
            _timer = new System.Timers.Timer
            {
                AutoReset = false,
                Interval = delay.TotalMilliseconds
            };
            _timer.Elapsed += (sender, e) => ApplicationDispatcher.BeginInvoke(action);
            _timer.Start();
        }
    }
}
