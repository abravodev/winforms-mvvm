namespace UserManager.Components
{
    public interface IMessageDialog
    {
        void Show(string message);
        void Show(string title, string message);
    }
}