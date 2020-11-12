using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace MvvmTools.Components
{
    public class MessageDialog : IMessageDialog
    {
        public DialogResult Show(string message) => MessageBox.Show(text: message);

        public DialogResult Show(string title, string message) => MessageBox.Show(text: message, caption: title);

        public DialogResult Show(string title, string message, MessageBoxIcon icon)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: icon,
                buttons: MessageBoxButtons.OK);

        public DialogResult Show(string title, string message, MessageBoxButtons buttons)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: MessageBoxIcon.Information,
                buttons: buttons);

        public DialogResult Show(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: icon,
                buttons: buttons);

        public DialogResult ShowError(string title, string message)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: MessageBoxIcon.Error,
                buttons: MessageBoxButtons.OK);

        public DialogResult Show(string title, ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            return ShowError(title, message);
        }

        public DialogResult Show(ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            return ShowError(title: "Validation errors", message);
        }
    }
}
