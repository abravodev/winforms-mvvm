using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvvmTools.Components
{
    public interface IMessageDialog
    {
        void Show(string message);
        void Show(string title, string message);
        void Show(string title, ICollection<ValidationResult> validationResults);
        void Show(ICollection<ValidationResult> validationResults);
    }
}