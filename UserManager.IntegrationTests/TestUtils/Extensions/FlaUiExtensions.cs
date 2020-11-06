using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UserManager.IntegrationTests.TestUtils.Extensions
{
    public static class FlaUiExtensions
    {
        public static Button GetButtonByName(this AutomationElement context, string buttonName)
        {
            return context.FindFirstDescendant(x => x.ByControlType(ControlType.Button).And(x.ByName(buttonName))).AsButton();
        }

        public static Button GetFirstButton(this AutomationElement context)
        {
            return context.FindFirstDescendant(x => x.ByControlType(ControlType.Button)).AsButton();
        }

        public static AutomationElement GetModalByTitle(this Window window, string name)
        {
            return window.ModalWindows.GetWithRetry(x => x.Title.Equals(name));
        }

        public static VisualForm AsForm(this AutomationElement context) => new VisualForm(context);

        private static TSource GetWithRetry<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Retry.WhileFalse(() => source.Any(predicate));
            return source.First(predicate);
        }
    }
}
