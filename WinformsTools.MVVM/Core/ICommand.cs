using System.Threading.Tasks;

namespace WinformsTools.MVVM.Core
{
    public interface ICommand
    {
        Task Execute();
    }

    public interface ICommand<T>
    {
        Task Execute(T parameter);
    }
}
