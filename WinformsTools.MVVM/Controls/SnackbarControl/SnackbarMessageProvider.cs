using WinformsTools.MVVM.Core;
using System.Windows.Forms;
using WinformsTools.MVVM.Navigation;

namespace WinformsTools.MVVM.Controls.SnackbarControl
{
    public class SnackbarMessageProvider : ISnackbarProvider
    {
        private readonly IRegisteredViews _registeredViews;

        public SnackbarMessageProvider(IRegisteredViews registeredViews)
        {
            _registeredViews = registeredViews;
        }

        public ISnackbarMessage Get<TViewModel>(TViewModel model) where TViewModel : IViewModel
        {
            var view = _registeredViews.Get(model);
            return new SnackbarMessage(view as Control);
        }
    }

    public interface ISnackbarProvider
    {
        ISnackbarMessage Get<TViewModel>(TViewModel model) where TViewModel : IViewModel;
    }
}
