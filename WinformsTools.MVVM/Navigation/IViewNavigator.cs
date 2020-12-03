using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Navigation
{
    public interface IViewNavigator
    {
        void Open<TViewModel>() where TViewModel : IViewModel;

        IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel;
    }
}