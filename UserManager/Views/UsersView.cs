using System.Windows.Forms;
using UserManager.ViewModels;
using MvvmTools.Bindings;
using MvvmTools.Validations;
using MvvmTools.Core;

namespace UserManager.Views
{
    public partial class UsersView : Form, IView<UsersViewModel>
    {
        public UsersViewModel ViewModel { get; }

        public UsersView(UsersViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .For(this.dgv_userlist, _ => _.Users)
                .For(this.bt_save, _ => _.Enabled, _ => _.CanCreateUser, dependsOn: _ => _.CreateUserInfo)
                .WithLoading(this.tlp_view, this.pb_loading, _ => _.Loading)
                .Click(this.bt_save, _ => _.CreateUserCommand)
                .Click(this.bt_cancel, _ => _.CancelUserCreationCommand);
            this.BindTo(ViewModel.CreateUserInfo)
                .For(this.tb_firstName, _ => _.FirstName)
                .For(this.tb_lastName, _ => _.LastName)
                .For(this.tb_email, _ => _.Email);
            this.WithValidation(ep_createUser)
                .On(this.tb_firstName).FieldRequired()
                .On(this.tb_lastName).FieldRequired()
                .On(this.tb_email).FieldRequired();
        }
    }
}
