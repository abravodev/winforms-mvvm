using System;
using System.Windows.Forms;
using WinformsTools.MVVM.Telemetry;

namespace WinformsTools.MVVM.Bindings
{
    public static class ClickBindingExtensions
    {
        public static ClickBind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Action onClick)
        {
            control.Click += (sender, args) => onClick();
            return ClickBinding(item, control);
        }

        public static ClickBind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Action<object, EventArgs> onClick)
        {
            control.Click += (sender, args) => onClick(sender, args);
            return ClickBinding(item, control);
        }

        public static ClickBind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Func<TBinding, Core.ICommand> command)
        {
            control.Click += (sender, e) => command(item._item).Execute();
            return ClickBinding(item, control);
        }

        public static ClickBind<TBinding> Click<TBinding, TParameter>(this Bind<TBinding> item, Control control, Func<TBinding, Core.ICommand<TParameter>> command, Func<TParameter> parameter)
        {
            control.Click += (sender, e) => command(item._item).Execute(parameter());
            return ClickBinding(item, control);
        }

        private static ClickBind<TBinding> ClickBinding<TBinding>(Bind<TBinding> item, Control control)
        {
            return new ClickBind<TBinding>(control, item);
        }

        public class ClickBind<TBinding> : Bind<TBinding>
        {
            private readonly Control _control;

            public ClickBind(Control control, Bind<TBinding> item) : base(item._item)
            {
                _control = control;
            }

            public Bind<TBinding> Log()
            {
                _control.Click += (sender, e) => Metrics.Log(new ClickMetric(_control));
                return this;
            }
        }
    }
}
