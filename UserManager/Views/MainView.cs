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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadLanguageMenu();
            SetTranslations();
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
        }

        private void btn_users_click(object sender, EventArgs e) => ViewModel.NavigateToUsersView();
    }
}
