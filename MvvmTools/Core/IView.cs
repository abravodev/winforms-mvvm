using System;

namespace MvvmTools.Core
{
    public interface IView<out TViewModel> where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }

        event EventHandler Load;

        void Show();
    }

    public interface IPartialView<TViewModel> : IView<TViewModel> where TViewModel : IViewModel
    {
        /// <summary>
        /// Set the given <paramref name="viewModel"/> in the view
        /// (If a view is loaded as part of another view, injection cannot happen at the constructor)
        /// </summary>
        /// <param name="viewModel"></param>
        void SetViewModel(TViewModel viewModel);
    }
}