using WinformsTools.MVVM.Core;
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
            var view = _registeredViews.GetControl(model);
            return new SnackbarMessage(view);
        }
    }

    public interface ISnackbarProvider
    {
        ISnackbarMessage Get<TViewModel>(TViewModel model) where TViewModel : IViewModel;
    }
}
