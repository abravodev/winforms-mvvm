using SimpleInjector;
using SimpleInjector.Diagnostics;
using System.Windows.Forms;
using UserManager.ViewModels;

namespace UserManager.Startup
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterView<TView, TViewModel>(this Container container)
            where TView : ContainerControl
            where TViewModel : IViewModel
        {
            container.Register(typeof(TViewModel));
            container.Register(typeof(TView));
            SupressTransientWarning<TView>(container);
        }

        /// <summary>
        /// <see href="https://stackoverflow.com/a/38421425"/>
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="container"></param>
        private static void SupressTransientWarning<TView>(Container container) where TView : ContainerControl
        {
            var viewRegistration = container.GetRegistration(typeof(TView)).Registration;
            viewRegistration.SuppressDiagnosticWarning(
                type: DiagnosticType.DisposableTransientComponent,
                justification: "Winforms registration supression");
        }
    }
}
