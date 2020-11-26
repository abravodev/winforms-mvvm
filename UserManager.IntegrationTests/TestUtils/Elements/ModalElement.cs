﻿using FlaUI.Core.AutomationElements;
using System.Collections.Generic;
using System.Linq;

namespace UserManager.IntegrationTests.TestUtils.Extensions
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

        public void Choose(DialogOption dialogOption) => Choose((System.Windows.Forms.DialogResult) dialogOption);

        public string GetMessage() => GetMessageElement().Text;

        public TextBox GetMessageElement()
        {
            return this.GetAllChildren<TextBox>().FirstOrDefault();
        }

        public List<FlaUI.Core.AutomationElements.Button> GetButtons()
        {
            return this.GetAllChildren<Button>().ToList();
        }

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
