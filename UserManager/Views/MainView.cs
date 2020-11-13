using MvvmTools.Bindings;
using MvvmTools.Core;
using System;
using System.Windows.Forms;
using UserManager.Resources;
using UserManager.ViewModels;

namespace UserManager.Views
{
    public partial class MainView : Form, IView<MainViewModel>
    {
        public MainViewModel ViewModel { get; }

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            InitializeDataBindings();
            SetTranslations();
        }

        private void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .Click(this.btn_users, ViewModel.NavigateToUsersView)
                .Click(this.btn_roles, ViewModel.NavigateToRolesView);
            LoadLanguageMenu();
        }

        private void LoadLanguageMenu()
        {
            this.tsmi_language.AddBinding(
                source: ViewModel.AvailableLanguages,
                onClicked: ViewModel.ChangeCurrentCulture);
        }

        private void SetTranslations()
        {
            this.btn_users.Text = General.Users;
            this.btn_roles.Text = General.Roles;
        }
    }
}
