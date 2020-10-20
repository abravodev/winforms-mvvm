using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace UserManager.Components
{
    public class MessageDialog : IMessageDialog
    {
        public void Show(string message) => MessageBox.Show(text: message);

        public void Show(string title, string message) => MessageBox.Show(text: message, caption: title);

        public void Show(string title, ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            Show(title, message);
        }

        public void Show(ICollection<ValidationResult> validationResults)
        {
            var message = string.Join("\n", validationResults.Select(x => x.ErrorMessage));
            Show(title: "Validation errors", message);
        }
    }
}
