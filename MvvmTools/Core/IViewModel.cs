using System.Threading.Tasks;

namespace MvvmTools.Core
{
    public interface IViewModel
    {
        Task Load();
    }
}