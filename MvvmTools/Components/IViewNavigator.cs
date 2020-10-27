using MvvmTools.Core;

namespace MvvmTools.Components
{
    public interface IViewNavigator
    {
        void Open<TViewModel>() where TViewModel : IViewModel;
    }
}