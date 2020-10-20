using System.Windows.Forms;

namespace UserManager.Components
{
    public class MessageDialog : IMessageDialog
    {
        public void Show(string message) => MessageBox.Show(text: message);

        public void Show(string title, string message) => MessageBox.Show(text: message, caption: title);
    }
}
