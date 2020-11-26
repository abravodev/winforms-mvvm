using MvvmTools.Core;

namespace MvvmTools.Navigation
{
    public interface IViewNavigator
    {
        void Open<TViewModel>() where TViewModel : IViewModel;

        IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel;
    }
}