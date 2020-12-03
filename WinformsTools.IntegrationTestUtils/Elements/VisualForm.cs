using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using WinformsTools.IntegrationTestUtils.Extensions;

namespace WinformsTools.IntegrationTestUtils.Elements
{
    /// <summary>
    /// Represents the idea of a form with user inputs
    /// </summary>
    public class VisualForm : AutomationElement
    {
        private readonly AutomationElement _form;

        public VisualForm(AutomationElement context) : base(context)
        {
            _form = context;
        }

        public VisualForm Fill(string inputName, string value)
        {
            var input = _form.Get<TextBox>(inputName, ControlType.Edit);
            input.Text = value;
            return this;
        }
    }
}
