using System;
using System.Windows.Threading;

namespace MvvmTools.Components
{
    /// <summary>
    /// Object to hold the main thread (UI Thread) in case you have to do something coming from a different thread
    /// It's a wrapper over <see cref="Dispatcher.CurrentDispatcher"/> to return always the main/UI thread
    /// </summary>
    public static class ApplicationDispatcher
    {
        private static Dispatcher _dispatcher;

        private static Dispatcher Dispatcher => _dispatcher ?? Dispatcher.CurrentDispatcher;

        /// <summary>
        /// Call this method at the startup of the application
        /// </summary>
        public static void Configure()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public static void Invoke(Action action) => Dispatcher.Invoke(action);

        public static DispatcherOperation BeginInvoke(Action action) => Dispatcher.BeginInvoke(action);
    }
}
