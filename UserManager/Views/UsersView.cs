using System.Windows.Forms;
using UserManager.ViewModels;
using MvvmTools.Bindings;
using MvvmTools.Core;

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

        private void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .For(this.dgv_userlist, _ => _.Users)
                .WithContextMenu(this.dgv_userlist,
                    ("Delete", ViewModel.DeleteUserCommand))
                .WithLoading(this.tlp_view, this.pb_loading, _ => _.Loading);
        }
    }
}
