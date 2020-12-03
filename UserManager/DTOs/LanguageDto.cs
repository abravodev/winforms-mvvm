using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Controls;
using System.Globalization;

namespace UserManager.DTOs
{
    public class LanguageDto : BindableObject, IMenuOption
    {
        private CultureInfo _cultureInfo;
        public CultureInfo Culture 
        {
            get { return _cultureInfo; }
            set { SetProperty(ref _cultureInfo, value); }
        }

        private bool _current;
        public bool Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        public string Text => this.Culture.EnglishName;

        public bool Checked => this.Current;
    }
}
