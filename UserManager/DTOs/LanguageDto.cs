using MvvmTools.Bindings;
using System.Globalization;

namespace UserManager.DTOs
{
    public class LanguageDto : BindableObject
    {
        private CultureInfo _cultureInfo;
        public CultureInfo Culture {
            get { return _cultureInfo; }
            set { SetProperty(ref _cultureInfo, value); }
        }

        private bool _current;
        public bool Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }
    }
}
