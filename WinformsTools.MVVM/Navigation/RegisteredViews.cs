using WinformsTools.MVVM.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace WinformsTools.MVVM.Navigation
{
    public interface IRegisteredViews
    {
        void Add<TViewModel>(IView<TViewModel> view) where TViewModel : IViewModel;

        IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel;
        
        Control GetControl<TViewModel>() where TViewModel : IViewModel;
    }

    public class RegisteredViews : IRegisteredViews
    {
        private List<object> _views = new List<object>();

        public void Add<TViewModel>(IView<TViewModel> view) where TViewModel : IViewModel
        {
            var controlView = view as Control;
            if (controlView == null)
            {
                throw new ArgumentException($"It must also be of type {typeof(Control)}", nameof(view));
            }

            controlView.Disposed += (sender, e) => _views.Remove(view);
            _views.Add(view);
        }

        public IView<TViewModel> Get<TViewModel>() where TViewModel : IViewModel
        {
            return _views
                .Where(x => x is IView<TViewModel>)
                .First() as IView<TViewModel>;
        }

        public Control GetControl<TViewModel>() where TViewModel : IViewModel
        {
            return Get<TViewModel>() as Control;
        }
    }
}
