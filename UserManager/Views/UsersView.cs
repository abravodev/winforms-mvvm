using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.Extensions;
using UserManager.ViewModels;

namespace UserManager.Views
{
    public partial class UsersView : Form
    {
        private UsersViewModel _viewModel;

        public UsersView(UsersViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            InitializeDataBindings();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AsyncHelpers.RunSync(() => _viewModel.Load());
            dgv_userlist.ResetDataSourceBinding();
        }

        private void InitializeDataBindings()
        {
            dgv_userlist.AddBinding(_viewModel.Users);
        }

        private async void btn_save_click(object sender, EventArgs e)
        {
            await _viewModel.CreateUser(new CreateUserDto
            {
                FirstName = this.tb_firstName.Text,
                LastName = this.tb_lastName.Text,
                Email = this.tb_email.Text
            });
            
            dgv_userlist.ResetDataSourceBinding();
            
            ClearCreateForm();
        }

        private void ClearCreateForm()
        {
            this.tb_firstName.Clear();
            this.tb_lastName.Clear();
            this.tb_email.Clear();
        }

        private void bt_cancel_click(object sender, EventArgs e)
        {
            ClearCreateForm();
        }
    }
}
