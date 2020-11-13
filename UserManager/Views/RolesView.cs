using MvvmTools.Bindings;
using MvvmTools.Core;
using System.Windows.Forms;
using UserManager.ViewModels;

namespace UserManager.Views
{
    public partial class RolesView : Form, IView<RolesViewModel>
    {
        public RolesViewModel ViewModel { get; }

        public RolesView(RolesViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            InitializeDataBindings();
        }

        private void InitializeDataBindings()
        {
            this.BindTo(ViewModel)
                .For(this.dgv_rolelist, _ => _.Roles)
                .WithLoading(this.tlp_view, this.pb_loading, _ => _.Loading);
        }
    }
}
