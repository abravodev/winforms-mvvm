using System;
using System.Windows.Forms;
using WinformsTools.MVVM.Components;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public partial class SnackbarControl : UserControl
    {
        private readonly Control _host;
        private System.Timers.Timer _timer;
        private int _initialHeight;

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
            _initialHeight = this.Height;
            this.Height = 0;
        }

        public void Show(string message, TimeSpan showTime)
        {
            lbl_snackbarMessage.Text = message;
            this.BringToFront();
            ShowAnimated();
            SetTimeout(showTime, HideAnimated);
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

        private void ShowAnimated()
        {
            Show();
            Enlarge();
        }

        private void HideAnimated()
        {
            Shrink();
            Hide();
        }

        private void Enlarge()
        {
            this.Height = 0;
            while (this.Height < _initialHeight)
            {
                this.Height++;
                Application.DoEvents();
            }
        }

        private void Shrink()
        {
            this.Height = _initialHeight;
            while (this.Height > 0)
            {
                this.Height--;
                Application.DoEvents();
            }
        }
    }
}
