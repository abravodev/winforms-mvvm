using System;
using System.Windows.Forms;
using UserManager.Startup;
using UserManager.Views;

namespace UserManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_users_click(object sender, EventArgs e)
        {
            var usersView = IoCConfig.Container.GetInstance<UsersView>();
            usersView.Show();
        }
    }
}
