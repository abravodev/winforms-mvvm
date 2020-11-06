using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace MvvmTools.Components
{
    public class MessageDialog : IMessageDialog
    {
        public void Show(string message) => MessageBox.Show(text: message);

        public void Show(string title, string message) => MessageBox.Show(text: message, caption: title);

        public void Show(string title, string message, MessageBoxIcon icon)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: icon,
                buttons: MessageBoxButtons.OK);

        public void Show(string title, string message, MessageBoxButtons buttons)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: MessageBoxIcon.Information,
                buttons: buttons);

        public void Show(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: icon,
                buttons: buttons);

        public void ShowError(string title, string message)
            => MessageBox.Show(
                caption: title,
                text: message,
                icon: MessageBoxIcon.Error,
                buttons: MessageBoxButtons.OK);

        public void Show(string title, ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            ShowError(title, message);
        }

        public void Show(ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            ShowError(title: "Validation errors", message);
        }
    }
}
