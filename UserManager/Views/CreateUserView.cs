using System.Windows.Forms;
using UserManager.ViewModels;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Validations;
using WinformsTools.MVVM.Core;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.Views
{
    public partial class CreateUserView : UserControl, IPartialView<CreateUserViewModel>
    {
        public CreateUserViewModel ViewModel { get; private set; }

        public CreateUserView()
        {
            InitializeComponent();
        }

        public void SetViewModel(CreateUserViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeDataBindings();
            Task.Run(async () => await ViewModel.Load());
        }

        public void InitializeDataBindings()
        {
            this.ConfigureDefaultAction(ViewModel.CreateUserCommand);
            this.bt_save.Enabled = false;
            this.BindTo(ViewModel)
                .For(this.bt_save, _ => _.Enabled, _ => _.CanCreateUser, dependsOn: _ => _.CreateUserInfo)
                .Click(this.bt_save, _ => _.CreateUserCommand)
                .Click(this.bt_cancel, _ => _.CancelUserCreationCommand);
            this.BindTo(ViewModel.CreateUserInfo)
                .For(this.tb_firstName, _ => _.FirstName)
                .For(this.tb_lastName, _ => _.LastName)
                .For(this.tb_email, _ => _.Email)
                .For(this.cb_role, _ => _.Role, 
                    ComboBoxSource.From(ViewModel.Roles)
                        .Display(_ => _.Name)
                        .Value(_ => _.Id)
                        .Default(_ => _.Id == Role.Basic.Id));
            this.WithValidation(ep_createUser)
                .On(this.tb_firstName).FieldRequired()
                .On(this.tb_lastName).FieldRequired()
                .On(this.tb_email).FieldRequired();
        }

        private void ConfigureDefaultAction(ICommand defaultAction)
        {
            this.Enter += (sender, args) => DefaultAction = defaultAction;
            this.Leave += (sender, args) => DefaultAction = null;
        }

        public ICommand DefaultAction { get; private set; }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                DefaultAction?.Execute();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
