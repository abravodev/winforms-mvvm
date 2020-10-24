using System.Threading.Tasks;

namespace MvvmTools.Core
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
