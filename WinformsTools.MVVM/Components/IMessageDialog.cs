using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Forms;

namespace WinformsTools.MVVM.Components
{
    public interface IMessageDialog
    {
        DialogResult Show(string message);
        DialogResult Show(string title, string message);

        DialogResult Show(string title, string message, MessageBoxIcon icon);
        DialogResult Show(string title, string message, MessageBoxButtons buttons);
        DialogResult Show(string title, string message, MessageBoxButtons buttons, MessageBoxIcon icon);

        DialogResult ShowError(string title, string message);

        DialogResult Show(string title, ICollection<ValidationResult> validationResults);
        DialogResult Show(ICollection<ValidationResult> validationResults);
    }
}