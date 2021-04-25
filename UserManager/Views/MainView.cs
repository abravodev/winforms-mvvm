using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Core;
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

        public void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .Click(this.btn_users, ViewModel.NavigateToUsersView).Log()
                .Click(this.btn_roles, ViewModel.NavigateToRolesView).Log();
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
