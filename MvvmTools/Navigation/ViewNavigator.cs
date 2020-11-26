using Easy.MessageHub;
using MvvmTools.Core;
using SimpleInjector;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvvmTools.Navigation
{
    public class ViewNavigator : IViewNavigator
    {
        private readonly Container _container;
        private readonly IMessageHub _eventAggregator;

        public ViewNavigator(Container container, IMessageHub eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public void Open<TViewModel>() where TViewModel : IViewModel => Get<TViewModel>().Show();

        public IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel
        {
            var viewCreator = GetViewCreator<TViewModel>();
            var view = viewCreator();
            SetupView(view);
            return view;
        }

        private Func<IView<TViewModel>> GetViewCreator<TViewModel>() where TViewModel : IViewModel
        {
            return _container.GetInstance<Func<IView<TViewModel>>>();
        }

        private void SetupView<TViewModel>(IView<TViewModel> view) where TViewModel : IViewModel
        {
            view.Load += async (sender, e) => await Task.Run(view.ViewModel.Load);
            SetupViewForms(view);
        }

        private void SetupViewForms<TViewModel>(IView<TViewModel> view) where TViewModel : IViewModel
        {
            var viewForm = view as Form;
            if (viewForm == null)
            {
                return;
            }

            viewForm.FormClosed += (sender, e) => _eventAggregator.Publish(ViewClosedEvent.Create(view));
        }
    }
}
