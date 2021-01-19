using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;
using WinformsTools.IntegrationTestUtils.Extensions;

namespace WinformsTools.IntegrationTestUtils.Elements
{
    public class ModalElement : AutomationElement
    {
        public ModalElement(AutomationElement automationElement) : base(automationElement)
        {
        }

        public void Choose(System.Windows.Forms.DialogResult dialogResult)
        {
            var automationId = DialogResultMapper.ToAutomationId(GetButtons().Count, dialogResult);
            FindFirstChild(x => x.ByAutomationId(automationId)).Click();
        }

        public void Choose(DialogOption dialogOption) => Choose((System.Windows.Forms.DialogResult)dialogOption);

        public string GetMessage() => GetMessageElement().Text;

        public TextBox GetMessageElement() => this.GetAllChildren<TextBox>().FirstOrDefault();

        public List<Button> GetButtons() => this.GetAllChildren<Button>().ToList();

        public System.Windows.Forms.DialogResult[] GetOptions()
        {
            var buttons = GetButtons();
            return buttons
                .Select(x => x.AutomationId)
                .Select(x => DialogResultMapper.FromAutomationId(buttons.Count, x))
                .ToArray();
        }
    }
}
