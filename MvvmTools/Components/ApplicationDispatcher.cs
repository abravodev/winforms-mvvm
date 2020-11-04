using System;
using System.Threading;

namespace MvvmTools.Components
{
    /// <summary>
    /// Object to hold the main thread (UI Thread) in case you have to do something coming from a different thread
    /// <see href="https://stackoverflow.com/a/26020749"/>
    /// </summary>
    public static class ApplicationDispatcher
    {
        private static SynchronizationContext _context;

        public static void Configure(SynchronizationContext context)
        {
            _context = context;
        }

        public static void Invoke(Action action)
        {
            if (_context == null)
            {
                action();
                return;
            }

            _context.Send(_ => action(), null);
        }

        public static void BeginInvoke(Action action)
        {
            if (_context == null)
            {
                action();
                return;
            }

            _context.Post(_ => action(), null);
        }
    }
}
