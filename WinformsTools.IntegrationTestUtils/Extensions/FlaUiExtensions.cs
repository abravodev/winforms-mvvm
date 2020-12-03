using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using WinformsTools.IntegrationTestUtils.Elements;

namespace WinformsTools.IntegrationTestUtils.Extensions
{
    public static class FlaUiExtensions
    {
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
