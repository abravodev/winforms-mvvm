using FlaUI.Core.AutomationElements;
using System;
using System.Windows.Forms;

namespace WinformsTools.IntegrationTestUtils.Elements
{
    /// <summary>
    /// Basic mapper between <see cref="DialogResult"/> and <see cref="AutomationElement.AutomationId"/>.
    /// It contains corner case for <see cref="MessageBoxButtons.OK"/> where there's a weird behavior:
    /// For [OK], the <see cref="AutomationElement.AutomationId"/> will be 2 (<see cref="DialogResult.Cancel"/>) instead of 1 (<see cref="DialogResult.OK"/>).
    /// Buttons's texts from <see cref="MessageBox"/> are translated by the system, so we cannot select by the text.
    /// </summary>
    public static class DialogResultMapper
    {
        public static string ToAutomationId(int numberOfOptions, DialogResult result)
        {
            if (result == DialogResult.OK && numberOfOptions == 1)
            {
                return DialogResult.Cancel.GetHashCode().ToString();
            }

            return result.GetHashCode().ToString();
        }

        public static DialogResult FromAutomationId(int numberOfOptions, string automationId)
        {
            var result = (DialogResult)Enum.Parse(typeof(DialogResult), automationId);
            if (result == DialogResult.Cancel && numberOfOptions == 1)
            {
                return DialogResult.OK;
            }
            return result;
        }

    }
}
