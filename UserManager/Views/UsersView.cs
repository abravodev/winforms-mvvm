using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.DTOs;
using UserManager.ViewModels;
using MvvmTools.Bindings;

namespace UserManager.Views
{
    public partial class UsersView : Form
    {
        private UsersViewModel _viewModel;
        private CreateUserDto _creationUser;

        public UsersView(UsersViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            InitializeDataBindings();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AsyncHelpers.RunSync(() => _viewModel.Load());
        }

        private void InitializeDataBindings()
        {
            _creationUser = new CreateUserDto();
            BindCreateUserForm();
            BindingUserList();
        }

        private void BindCreateUserForm()
        {
            this.tb_firstName.AddBinding(_creationUser, nameof(_creationUser.FirstName));
            this.tb_lastName.AddBinding(_creationUser, nameof(_creationUser.LastName));
            this.tb_email.AddBinding(_creationUser, nameof(_creationUser.Email));
        }

        private void BindingUserList()
        {
            dgv_userlist.AddBinding(_viewModel.Users);
        }

        private async void btn_save_click(object sender, EventArgs e)
        {
            await _viewModel.CreateUser(_creationUser);            
            ClearCreateForm();
        }

        private void ClearCreateForm()
        {
            _creationUser.FirstName = string.Empty;
            _creationUser.LastName = string.Empty;
            _creationUser.Email = string.Empty;
        }

        private void bt_cancel_click(object sender, EventArgs e)
        {
            ClearCreateForm();
        }
    }
}
