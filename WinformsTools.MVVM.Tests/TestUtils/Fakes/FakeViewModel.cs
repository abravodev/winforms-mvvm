using System.Threading.Tasks;
using WinformsTools.MVVM.Core;

namespace WinformsTools.MVVM.Tests.TestUtils.Fakes
{
    public class FakeViewModel : IViewModel
    {
        public Task Load() => Task.CompletedTask;
    }
}
