using FontAwesome.Sharp;
using System.Drawing;

namespace MvvmTools.Bindings
{
    public static class IconExtensions
    {
        public static Bitmap GetImage(this IconChar icon)
        {
            return icon.ToBitmap(color: Color.Black, size: 48, rotation: 0, flip: FlipOrientation.Normal);
        }
    }
}
