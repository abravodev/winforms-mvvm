using System;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.Extensions;
using UserManager.ViewModels;

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
            (dgv_userlist.DataSource as BindingSource).ResetBindings(false);
        }

        private void InitializeDataBindings()
        {
            dgv_userlist.AddBinding(_viewModel.Users);
        }
    }
}
