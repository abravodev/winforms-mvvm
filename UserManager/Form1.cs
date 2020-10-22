using System;
using System.Windows.Forms;
using UserManager.Views;

namespace UserManager
{
    public partial class Form1 : Form
    {
        private readonly Func<UsersView> _usersView;

        public Form1(Func<UsersView> usersView)
        {
            InitializeComponent();
            _usersView = usersView;
        }

        private void btn_users_click(object sender, EventArgs e)
        {
            var usersView = _usersView();
            usersView.Show();
        }
    }
}
