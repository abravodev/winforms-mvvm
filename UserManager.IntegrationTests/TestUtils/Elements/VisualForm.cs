using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;

namespace UserManager.IntegrationTests.TestUtils.Elements
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
            var input = _form.FindFirstDescendant(x => x.ByControlType(ControlType.Edit).And(x.ByName(inputName))).AsTextBox();
            input.Text = value;
            return this;
        }
    }
}
