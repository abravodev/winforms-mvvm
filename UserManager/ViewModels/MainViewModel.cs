using AutoMapper.Internal;
using MvvmTools.Bindings;
using MvvmTools.Components;
using MvvmTools.Core;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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

        public BindingList<LanguageDto> AvailableLanguages { get; }

        public MainViewModel(ISettingProvider settingProvider, IMessageDialog messageDialog, IViewNavigator viewNavigator)
        {
            _settingProvider = settingProvider;
            _messageDialog = messageDialog;
            _viewNavigator = viewNavigator;
            AvailableLanguages = new BindingList<LanguageDto>();
        }

        public async Task Load() 
        {
            var languages = GetAvailableLanguages();
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

        public void ChangeCurrentCulture(LanguageDto selected)
        {
            var selectedLanguage = this.AvailableLanguages.FirstOrDefault(x => x.Culture.Equals(selected.Culture));
            if (selectedLanguage.Current)
            {
                return;
            }

            this.AvailableLanguages.ForAll(x => x.Current = false);
            selectedLanguage.Current = true;

            _settingProvider.SetCurrentCulture(selectedLanguage.Culture);

            _messageDialog.Show(
                title: General.LanguageChangeTitle,
                message: General.LanguageChangeMessage);
        }

        public void NavigateToUsersView() => _viewNavigator.Open<UsersViewModel>();
    }
}
