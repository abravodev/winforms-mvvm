using AutoMapper.Internal;
using MvvmTools.Core;
using System;
using System.Linq;
using System.Windows.Forms;
using UserManager.BusinessLogic.Common;
using UserManager.DTOs;
using UserManager.Resources;
using UserManager.ViewModels;

namespace UserManager.Views
{
    public partial class MainView : Form, IView<MainViewModel>
    {
        private readonly MainViewModel _viewModel;

        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AsyncHelpers.RunSync(_viewModel.Load);
            LoadLanguageMenu();
            SetTranslations();
        }

        private void LoadLanguageMenu()
        {
            var menuItems = _viewModel
                .AvailableLanguages
                .Select(MapToMenuItem)
                .ToArray();
            this.tsmi_language.DropDownItems.AddRange(menuItems);
            this.tsmi_language.DropDownItemClicked += OnLanguageSelected;
            _viewModel.LanguageChanged += _viewModel_LanguageChanged;
        }

        private ToolStripMenuItem MapToMenuItem(LanguageDto x)
        {
            return new ToolStripMenuItem
            {
                Text = x.Culture.EnglishName,
                Tag = x,
                Checked = x.Current
            };
        }

        private void _viewModel_LanguageChanged(object sender, LanguageChangedEventArgs e)
        {
            var menuOptions = this.tsmi_language.DropDownItems.Cast<ToolStripMenuItem>();
            menuOptions.ForAll(x =>
            {
                x.Checked = false;
                (x.Tag as LanguageDto).Current = false; 
            });
            var menuItem = menuOptions.FirstOrDefault(x => x.Tag.Equals(e.SelectedLanguage));
            menuItem.Checked = true;
        }

        private void SetTranslations()
        {
            this.btn_users.Text = General.Users;
        }

        private void btn_users_click(object sender, EventArgs e) => _viewModel.NavigateToUsersView();

        private void OnLanguageSelected(object sender, EventArgs e)
        {
            var selectedItem = (e as ToolStripItemClickedEventArgs).ClickedItem as ToolStripMenuItem;
            _viewModel.ChangeCurrentCulture(selectedItem.Tag as LanguageDto);
        }
    }
}
