using Easy.MessageHub;
using WinformsTools.MVVM.Core;
using SimpleInjector;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformsTools.MVVM.Telemetry;

namespace WinformsTools.MVVM.Navigation
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
            view.Load += (sender, e) => Metrics.Log(new ViewEnteredMetric<TViewModel>(view));
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
