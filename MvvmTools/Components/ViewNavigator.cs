using MvvmTools.Core;
using SimpleInjector;
using System;
using System.Threading.Tasks;

namespace MvvmTools.Components
{
    public class ViewNavigator : IViewNavigator
    {
        private readonly Container _container;

        public ViewNavigator(Container container)
        {
            _container = container;
        }

        public void Open<TViewModel>() where TViewModel : IViewModel => Get<TViewModel>().Show();

        public IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel
        {
            var viewCreator = _container.GetInstance<Func<IView<TViewModel>>>();
            var view = viewCreator();

            view.Load += async (sender, e) => await Task.Run(view.ViewModel.Load);

            return view;
        }
    }
}
