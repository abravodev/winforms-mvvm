using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace MvvmTools.Components
{
    public interface IMessageDialog
    {
        void Show(string message);
        void Show(string title, string message);

        void Show(string title, string message, MessageBoxIcon icon);
        void Show(string title, string message, MessageBoxButtons buttons);
        void Show(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon);

        void ShowError(string title, string message);

        void Show(string title, ICollection<ValidationResult> validationResults);
        void Show(ICollection<ValidationResult> validationResults);
    }
}