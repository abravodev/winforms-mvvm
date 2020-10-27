using MvvmTools.Components;
using MvvmTools.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UserManager.DTOs;
using UserManager.Providers;
using UserManager.Resources;

namespace UserManager.ViewModels
{
    public class MainViewModel : IViewModel
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IMessageDialog _messageDialog;
        private readonly IViewNavigator _viewNavigator;

        public List<LanguageDto> AvailableLanguages { get; }

        public event EventHandler<LanguageChangedEventArgs> LanguageChanged;

        public MainViewModel(ISettingProvider settingProvider, IMessageDialog messageDialog, IViewNavigator viewNavigator)
        {
            _settingProvider = settingProvider;
            _messageDialog = messageDialog;
            _viewNavigator = viewNavigator;
            AvailableLanguages = new List<LanguageDto>();
        }

        public async Task Load() 
        {
            var languages = GetAvailableLanguages();
            AvailableLanguages.Clear();
            AvailableLanguages.AddRange(languages);
        }

        private List<LanguageDto> GetAvailableLanguages()
        {
            var currentCulture = _settingProvider.GetCurrentCulture();
            return new List<LanguageDto>
            {
                CreateLanguageDto(language: "en", currentCulture),
                CreateLanguageDto(language: "es", currentCulture)
            };
        }

        private LanguageDto CreateLanguageDto(string language, CultureInfo defaultUserCulture)
        {
            var itemCulture = new CultureInfo(language);
            return new LanguageDto
            {
                Culture = itemCulture,
                Current = itemCulture.Equals(defaultUserCulture)
            };
        }

        public void ChangeCurrentCulture(LanguageDto selectedLanguage)
        {
            if (selectedLanguage.Current)
            {
                return;
            }

            selectedLanguage.Current = true;

            _settingProvider.SetCurrentCulture(selectedLanguage.Culture);
            LanguageChanged?.Invoke(this, new LanguageChangedEventArgs(selectedLanguage));

            _messageDialog.Show(
                title: General.LanguageChangeTitle,
                message: General.LanguageChangeMessage);
        }

        public void NavigateToUsersView() => _viewNavigator.Open<UsersViewModel>();
    }

    public class LanguageChangedEventArgs : EventArgs
    {
        public LanguageChangedEventArgs(LanguageDto language)
        {
            SelectedLanguage = language;
        }

        public LanguageDto SelectedLanguage { get; set; }
    }
}
