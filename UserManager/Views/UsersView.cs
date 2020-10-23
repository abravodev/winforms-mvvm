using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.ViewModels;
using MvvmTools.Bindings;
using MvvmTools.Validations;

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
        }

        private void InitializeDataBindings()
        {
            this.BindTo(_viewModel)
                .For(this.dgv_userlist, _ => _.Users)
                .For(this.bt_save, _ => _.Enabled, _ => _.CanCreateUser, dependsOn: _ => _.CreateUserInfo)
                .Click(this.bt_save, CreateUser)
                .Click(this.bt_cancel, ClearCreateForm);
            this.BindTo(_viewModel.CreateUserInfo)
                .For(this.tb_firstName, _ => _.FirstName)
                .For(this.tb_lastName, _ => _.LastName)
                .For(this.tb_email, _ => _.Email);
            this.WithValidation(ep_createUser)
                .On(this.tb_firstName).FieldRequired()
                .On(this.tb_lastName).FieldRequired()
                .On(this.tb_email).FieldRequired();
        }

        private async void CreateUser()
        {
            await _viewModel.CreateUser();
            ClearCreateForm();
        }

        private void ClearCreateForm()
        {
            _viewModel.CreateUserInfo.FirstName = string.Empty;
            _viewModel.CreateUserInfo.LastName = string.Empty;
            _viewModel.CreateUserInfo.Email = string.Empty;
        }
    }
}
