using MvvmTools.Core;
using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.DTOs;
using UserManager.Resources;
using UserManager.ViewModels;

namespace UserManager.Views
{
    public partial class MainView : Form, IView<MainViewModel>
    {
        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AsyncHelpers.RunSync(_viewModel.Load);
            LoadLanguageMenu();
            SetTranslations();
        }

        private void LoadLanguageMenu()
        {
            this.tsmi_language.AddBinding(
                source: _viewModel.AvailableLanguages,
                mapToMenuItem: MapToMenuItem,
                onClicked: _viewModel.ChangeCurrentCulture);
        }

        private ToolStripMenuItem MapToMenuItem(LanguageDto x)
        {
            return new ToolStripMenuItem
            {
                Text = x.Culture.EnglishName,
                Checked = x.Current
            };
        }

        private void SetTranslations()
        {
            this.btn_users.Text = General.Users;
        }

        private void btn_users_click(object sender, EventArgs e) => _viewModel.NavigateToUsersView();
    }
}
