using MvvmTools.Core;
using SimpleInjector;
using System;

namespace MvvmTools.Components
{
    public class ViewNavigator : IViewNavigator
    {
        private readonly Container _container;

        public ViewNavigator(Container container)
        {
            _container = container;
        }

        public void Open<TViewModel>() where TViewModel : IViewModel
        {
            var viewCreator = _container.GetInstance<Func<IView<TViewModel>>>();
            var view = viewCreator();
            view.Show();
        }
    }
}
