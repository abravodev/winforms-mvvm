﻿using System.Windows.Forms;
using UserManager.ViewModels;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Core;
using UserManager.Resources;
using FontAwesome.Sharp;

namespace UserManager.Views
{
    public partial class UsersView : Form, IView<UsersViewModel>
    {
        public UsersViewModel ViewModel { get; }

        public UsersView(UsersViewModel viewModel, CreateUserViewModel createUserView)
        {
            InitializeComponent();
            ViewModel = viewModel;
            this.createUserView.SetViewModel(createUserView);
            InitializeDataBindings();
        }

        public void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .For(this.dgv_userlist, _ => _.Users)
                .WithContextMenu(this.dgv_userlist,
                    MenuOption.Create(General.Delete, ViewModel.DeleteUserCommand, IconChar.Times))
                .WithLoading(this.tlp_view, this.pb_loading, _ => _.Loading);
            dgv_userlist.RowsAdded += (sender, e) => ShowUserCount();
            dgv_userlist.RowsRemoved += (sender, e) => ShowUserCount();
        }

        private void ShowUserCount()
        {
            this.lbl_userCount.Text = $"Total users: {this.dgv_userlist.RowCount}";
        }
    }
}
