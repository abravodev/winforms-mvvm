using FontAwesome.Sharp;
using WinformsTools.MVVM.Core;
using System.Drawing;

namespace WinformsTools.MVVM.Bindings
{
    public static class MenuOption
    {
        public static MenuOption<T> Create<T>(string name, ICommand<T> command) => new MenuOption<T>(name, command, null);

        public static MenuOption<T> Create<T>(string name, ICommand<T> command, Image image) => new MenuOption<T>(name, command, image);

        public static MenuOption<T> Create<T>(string name, ICommand<T> command, IconChar icon)
        {
            return new MenuOption<T>(name, command, icon.GetImage());
        }
    }

    public class MenuOption<T>
    {
        public MenuOption(string name, ICommand<T> command, Image image)
        {
            this.Name = name;
            this.Command = command;
            this.Image = image;
        }

        public string Name { get; }
        public ICommand<T> Command { get; }
        public Image Image { get; }
    }
}
