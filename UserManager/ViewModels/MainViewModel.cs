using AutoMapper.Internal;
using WinformsTools.MVVM.Components;
using WinformsTools.MVVM.Core;
using WinformsTools.Common.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UserManager.DTOs;
using UserManager.Providers;
using UserManager.Resources;
using Easy.MessageHub;
using WinformsTools.MVVM.Navigation;

namespace UserManager.ViewModels
{
    public class MainViewModel : IViewModel
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IMessageDialog _messageDialog;
        private readonly IViewNavigator _viewNavigator;
        private readonly ISet<string> _openedWindows = new HashSet<string>();
        private readonly IMessageHub _eventAggregator;

        public BindingList<LanguageDto> AvailableLanguages { get; }

        public MainViewModel(ISettingProvider settingProvider, IMessageDialog messageDialog, IViewNavigator viewNavigator, IMessageHub eventAggregator)
        {
            _settingProvider = settingProvider;
            _messageDialog = messageDialog;
            _viewNavigator = viewNavigator;
            _eventAggregator = eventAggregator;
            SubscribeToEvents();
            AvailableLanguages = new BindingList<LanguageDto>();
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.Subscribe<ViewClosedEvent>(ViewClosed);
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

        public void NavigateToUsersView() => NavigateTo<UsersViewModel>();

        public void NavigateToRolesView() => NavigateTo<RolesViewModel>();

        private void NavigateTo<TViewModel>() where TViewModel : IViewModel
        {
            if (_openedWindows.Contains(typeof(TViewModel).Name))
            {
                _messageDialog.ShowError(
                    title: General.AttentionTitle,
                    message: General.CannotOpenWindowsTwice);
                return;
            }
            _openedWindows.Add(typeof(TViewModel).Name);
            _viewNavigator.Open<TViewModel>();
        }

        private void ViewClosed(ViewClosedEvent evt)
        {
            _openedWindows.Remove(evt.ViewModelName);
        }
    }
}
