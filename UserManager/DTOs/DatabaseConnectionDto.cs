using WinformsTools.MVVM.Bindings;

namespace UserManager.DTOs
{
    public class DatabaseConnectionDto : BindableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private bool _connected;
        public bool Connected
        {
            get { return _connected; }
            set { SetProperty(ref _connected, value); }
        }
    }
}
