using System;
using System.Windows.Forms;

namespace MvvmTools.Bindings
{
    public static class ClickBindingExtensions
    {
        public static Bind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Action onClick)
        {
            control.Click += (sender, args) => onClick();
            return item;
        }

        public static Bind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Action<object, EventArgs> onClick)
        {
            control.Click += (sender, args) => onClick(sender, args);
            return item;
        }

        public static Bind<TBinding> Click<TBinding>(this Bind<TBinding> item, Control control, Func<TBinding, Core.ICommand> command)
        {
            control.Click += (sender, e) => command(item._item).Execute();
            return item;
        }

        public static Bind<TBinding> Click<TBinding, TParameter>(this Bind<TBinding> item, Control control, Func<TBinding, Core.ICommand<TParameter>> command, Func<TParameter> parameter)
        {
            control.Click += (sender, e) => command(item._item).Execute(parameter());
            return item;
        }
    }
}
