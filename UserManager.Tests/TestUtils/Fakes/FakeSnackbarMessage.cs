using NSubstitute;
using WinformsTools.MVVM.Controls.SnackbarControl;
using WinformsTools.MVVM.Core;

namespace UserManager.Tests.TestUtils.Fakes
{
    public static class FakeSnackbarMessage
    {
        public static (ISnackbarProvider Provider, ISnackbarMessage Message) Create()
        {
            var snackbarMessage = Substitute.For<ISnackbarMessage>();
            return Create(snackbarMessage);
        }

        public static (ISnackbarProvider Provider, ISnackbarMessage Message) Create(ISnackbarMessage snackbarMessage)
        {
            var snackbarProvider = Substitute.For<ISnackbarProvider>();
            snackbarProvider.Get<IViewModel>(null).ReturnsForAnyArgs(snackbarMessage);
            return (snackbarProvider, snackbarMessage);
        }
    }
}
