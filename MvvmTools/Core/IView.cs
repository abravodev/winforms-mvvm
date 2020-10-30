using System;

namespace MvvmTools.Core
{
    public interface IView<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }

        event EventHandler Load;

        void Show();
    }
}