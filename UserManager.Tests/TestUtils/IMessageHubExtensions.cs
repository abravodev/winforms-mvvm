using Easy.MessageHub;
using NSubstitute;
using System;

namespace UserManager.Tests.TestUtils
{
    public static class IMessageHubExtensions
    {
        /// <summary>
        /// Get the action that would be executed when the event <typeparamref name="TEvent"/> would be published
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="messageHub"></param>
        /// <param name="subscribedAction"></param>
        public static void GetAction<TEvent>(this IMessageHub messageHub, Action<Action<TEvent>> subscribedAction)
        {
            messageHub
                .When(_ => _.Subscribe(Arg.Any<Action<TEvent>>()))
                .Do(_ => subscribedAction(_.Arg<Action<TEvent>>()));
        }
    }
}
