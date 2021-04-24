using System;
using System.Reflection;

namespace WinformsTools.Common.Helpers
{
    public static class MagicToolbox
    {
        public static void SetProperty<T, TValue>(T obj, string name, TValue value)
        {
            var property = typeof(T).GetProperty(name);
            property.SetValue(obj, value);
        }

        /// <summary>
        /// Get <see cref="EventHandler"/> named <paramref name="eventHandlerName"/> from <paramref name="obj"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="eventHandlerName"></param>
        /// <returns></returns>
        public static EventHandlerInfo GetEvent<T>(T obj, string eventHandlerName)
        {
            var field = typeof(T).GetField(eventHandlerName, BindingFlags.NonPublic | BindingFlags.Instance);
            var eventDelegate = field.GetValue(obj) as MulticastDelegate;
            return eventDelegate != null ? new EventHandlerInfo(eventDelegate) : default(EventHandlerInfo);
        }

        public class EventHandlerInfo
        {
            private MulticastDelegate _eventDelegate;

            public EventHandlerInfo(MulticastDelegate eventDelegate)
            {
                _eventDelegate = eventDelegate;
            }

            public void Invoke<TEventArgs>(object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
            {
                foreach (var eventHandler in _eventDelegate.GetInvocationList())
                {
                    eventHandler.Method.Invoke(
                    eventHandler.Target,
                    new object[] { sender, eventArgs });
                }
            }
        }
    }
}
