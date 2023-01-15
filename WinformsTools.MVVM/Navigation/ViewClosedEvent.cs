using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Navigation
{
    public class ViewClosedEvent
    {
        public string ViewName { get; }

        public string ViewModelName { get; }

        public ViewClosedEvent(string viewName, string viewModelName)
        {
            ViewName = viewName;
            ViewModelName = viewModelName;
        }

        public static ViewClosedEvent Create<TViewModel>(IView<TViewModel> view) where TViewModel : IViewModel
        {
            return new ViewClosedEvent(view.GetType().Name, typeof(TViewModel).Name);
        }

        public static ViewClosedEvent Create<TView, TViewModel>()
            where TView : IView<TViewModel>
            where TViewModel : IViewModel
        {
            return new ViewClosedEvent(typeof(TView).Name, typeof(TViewModel).Name);
        }
    }
}