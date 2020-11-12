using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UserManager.IntegrationTests.TestUtils.Extensions
{
    public class ModalElement : AutomationElement
    {
        public ModalElement(AutomationElement automationElement) : base(automationElement)
        {
        }

        public void Choose(DialogResult dialogResult)
        {
            var automationId = DialogResultMapper.ToAutomationId(GetButtons().Count, dialogResult);
            FindFirstChild(x => x.ByAutomationId(automationId)).Click();
        }

        public List<FlaUI.Core.AutomationElements.Button> GetButtons()
        {
            return FindAllChildren(x => x.ByControlType(FlaUI.Core.Definitions.ControlType.Button))
                .Select(x => x.AsButton())
                .ToList();
        }

        public DialogResult[] GetOptions()
        {
            var buttons = GetButtons();
            return buttons
                .Select(x => x.AutomationId)
                .Select(x => DialogResultMapper.FromAutomationId(buttons.Count, x))
                .ToArray();
        }
    }
}
