﻿using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.ViewModels;
using MvvmTools.Bindings;

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
                .For(this.bt_save, _ => _.Enabled, _ => _.CanCreateUser, dependsOn: _ => _.CreateUserInfo);
            this.BindTo(_viewModel.CreateUserInfo)
                .For(this.tb_firstName, _ => _.FirstName)
                .For(this.tb_lastName, _ => _.LastName)
                .For(this.tb_email, _ => _.Email);
        }

        private async void btn_save_click(object sender, EventArgs e)
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

        private void bt_cancel_click(object sender, EventArgs e)
        {
            ClearCreateForm();
        }
    }
}
