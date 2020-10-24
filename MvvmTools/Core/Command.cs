using System;
using System.Threading.Tasks;

namespace MvvmTools.Core
{
    public class Command : ICommand
    {
        private readonly Func<Task> _command;

        public Command(Func<Task> command)
        {
            _command = command;
        }

        public static ICommand From(Action command)
        {
            return new Command(() => { command(); return Task.CompletedTask; });
        }

        public static ICommand From(Func<Task> command) => new Command(command);

        public static ICommand<T> From<T>(Func<T, Task> command) => new Command<T>(command);

        public static ICommand<T> From<T>(Action<T> command) => new Command<T>(_ => Task.FromResult(command));

        public Task Execute() => _command();
    }

    public class Command<T> : ICommand<T>
    {
        private readonly Func<T, Task> _command;

        public Command(Func<T, Task> command)
        {
            _command = command;
        }

        public Task Execute(T parameter) => _command(parameter);
    }
}
