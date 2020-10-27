namespace MvvmTools.Core
{
    public interface IView<TViewModel> where TViewModel : IViewModel
    {
        void Show();
    }
}