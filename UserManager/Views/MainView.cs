using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Core;
using System.Windows.Forms;
using UserManager.Resources;
using UserManager.ViewModels;
using System.Drawing;
using UserManager.DTOs;
using System;
using WinformsTools.MVVM.Controls;

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

        public void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .Click(this.btn_users, ViewModel.NavigateToUsersView).Log()
                .Click(this.btn_roles, ViewModel.NavigateToRolesView).Log()
                .For(this.lbl_databaseConnectionString, _ => _.Text, _ => _.DatabaseConnection.Name)
                .For(this.lbl_databaseConnectionString, _ => _.ForeColor)
                    .WithConverter(_ => _.DatabaseConnection.ConnectionStatus, new StatusToColorConverter())
                .WithTooltipOn(this.lbl_databaseConnectionString, _ => _.DatabaseConnection.Server, dependsOn: _ => _.DatabaseConnection)
                .For(this.ic_connectionStatus, _ => _.IconColor)
                    .WithConverter(_ => _.DatabaseConnection.ConnectionStatus, new StatusToColorConverter());
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

        private class StatusToColorConverter : SourceToControlConverter<ConnectionStatus, Color>
        {
            public override Color Convert(ConnectionStatus source)
            {
                switch (source)
                {
                    case ConnectionStatus.Connecting: return Color.DarkOrange;
                    case ConnectionStatus.Connected: return Color.Green;
                    case ConnectionStatus.Disconnected: return Color.Red;
                    default: throw new ArgumentException($"Invalid value: {source}", nameof(source));
                }
            }
        }
    }
}
