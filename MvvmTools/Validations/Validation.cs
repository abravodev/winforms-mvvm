using System;
using System.Windows.Forms;

namespace MvvmTools.Validations
{
    public class Validation<TControl> : Validation
        where TControl : TextBox
    {
        private readonly TControl _control;
        private readonly ErrorProvider _errorProvider;

        public Validation(ErrorProvider errorProvider, TControl control) : base(errorProvider)
        {
            _control = control;
            _errorProvider = errorProvider;
        }

        public Validation<TControl> FieldRequired() => WithValidation(_ => _.TextLength == 0, "Field is required");

        private Validation<TControl> WithValidation(Func<TextBox, bool> isValid, string errorMessage)
        {
            _control.Validated += (sender, args) => Validate(_control, isValid, errorMessage);
            return this;
        }

        private void Validate(TextBox textBox, Func<TextBox, bool> isValid, string errorMessage)
        {
            _errorProvider.SetIconPadding(textBox, -20);
            _errorProvider.SetError(textBox, !isValid(textBox) ? string.Empty : errorMessage);
        }
    }

    public class Validation
    {
        private readonly ErrorProvider _errorProvider;

        public Validation(ErrorProvider errorProvider)
        {
            _errorProvider = errorProvider;
        }

        public Validation<TextBox> On(TextBox textBox)
        {
            return new Validation<TextBox>(_errorProvider, textBox);
        }
    }

    public static class ValidationExtensions
    {
        public static Validation WithValidation<TView>(this TView view, ErrorProvider errorProvider)
            where TView : ContainerControl
        {
            return new Validation(errorProvider);
        }
    }
}
