using AutoMapper.Internal;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using UserManager.Resources;
using UserManager.Views;

namespace UserManager
{
    public partial class Form1 : Form
    {
        private readonly Func<UsersView> _usersView;

        public Form1(Func<UsersView> usersView)
        {
            InitializeComponent();
            _usersView = usersView;
            var userCulture = Properties.Settings.Default.UserCulture;
            LoadLanguageMenu(new CultureInfo(userCulture));
            SetTranslations();
        }

        private void LoadLanguageMenu(CultureInfo userCulture)
        {
            this.tsmi_language.DropDownItems.AddRange(new ToolStripItem[]
            {
                CreateMenuItem(language: "en", userCulture),
                CreateMenuItem(language: "es", userCulture),
            });
            this.tsmi_language.DropDownItemClicked += onLanguageSelected;
        }

        private ToolStripMenuItem CreateMenuItem(string language, CultureInfo defaultUserCulture)
        {
            var itemCulture = new CultureInfo(language);
            return new ToolStripMenuItem
            {
                Text = itemCulture.EnglishName,
                Tag = itemCulture,
                Checked = itemCulture.Equals(defaultUserCulture)
            };
        }

        private void SetTranslations()
        {
            this.btn_users.Text = General.Users;
        }

        private void btn_users_click(object sender, EventArgs e)
        {
            var usersView = _usersView();
            usersView.Show();
        }

        private void onLanguageSelected(object sender, EventArgs e)
        {
            var menuItem = (e as ToolStripItemClickedEventArgs).ClickedItem as ToolStripMenuItem;
            this.tsmi_language.DropDownItems.Cast<ToolStripMenuItem>().ForAll(x => x.Checked = false);
            if (menuItem.Checked)
            {
                return; // Already selected
            }

            menuItem.Checked = true;
            var language = menuItem.Tag as CultureInfo;
            Properties.Settings.Default.UserCulture = language.Name;
            Properties.Settings.Default.Save();
            MessageBox.Show(
                text: General.LanguageChangeMessage,
                caption: General.LanguageChangeTitle);
        }
    }
}
