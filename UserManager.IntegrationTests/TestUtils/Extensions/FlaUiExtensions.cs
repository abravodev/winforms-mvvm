using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using System.Linq;
using UserManager.IntegrationTests.TestUtils.Elements;

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
            return Retry.WhileNull(() => window.ModalWindows.SingleOrDefault(x => x.Title.Equals(name))).Result;
        }

        public static void SelectMenuItem(this Menu menu, string menuItem)
        {
            menu.Items[menuItem].Focus();
            Keyboard.Type(VirtualKeyShort.ENTER);
            // Here, it should be "menu.Items[menuItem].Click()", but it's not working. Documented at:
            // https://github.com/FlaUI/FlaUI/issues/82
            // https://github.com/FlaUI/FlaUI/pull/153
            // https://github.com/FlaUI/FlaUI/issues/203
        }

        public static VisualForm AsForm(this AutomationElement context) => new VisualForm(context);

        public static ModalElement AsModal(this AutomationElement context) => context != null ? new ModalElement(context) : null;
    }
}
