using MvvmTools.Core;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Windows.Forms;

namespace MvvmTools.Common
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterView<TView, TViewModel>(this Container container)
            where TView : ContainerControl
            where TViewModel : IViewModel
        {
            container.Register(typeof(TViewModel));
            container.RegisterView<TView>();
        }
        
        public static void RegisterView<TView>(this Container container)
            where TView : ContainerControl
        {
            container.Register(typeof(TView));
            SupressTransientWarning<TView>(container);
            container.Register<Func<TView>>(() => () => container.GetInstance<TView>());
            SupressTransientWarning<Func<TView>>(container);
        }

        /// <summary>
        /// <see href="https://stackoverflow.com/a/38421425"/>
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="container"></param>
        private static void SupressTransientWarning<TView>(Container container)
        {
            var viewRegistration = container.GetRegistration(typeof(TView)).Registration;
            viewRegistration.SuppressDiagnosticWarning(
                type: DiagnosticType.DisposableTransientComponent,
                justification: "Winforms registration supression");
        }
    }
}
