using FlaUI.Core.Tools;
using System.Threading;

namespace WinformsTools.IntegrationTestUtils.Extensions
{
    /// <summary>
    /// Inspired by <see cref="FlaUI.Core.Tools.LocalizedStrings"/>
    /// </summary>
    public static class CustomLocalizedStrings
    {
        static CustomLocalizedStrings()
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                // TODO: Create PR for changing original code to include Spanish
                case "es":
                    WinFormsUIA2VerticalScrollBarName = "Barra de desplazamiento vertical";
                    WinFormsUIA3VerticalScrollBarName = "Vertical";  // TODO: Find out Spanish version
                    WinFormsUIA2HorizontalScrollBarName = "Horizontal Scroll Bar";  // TODO: Find out Spanish version
                    WinFormsUIA3HorizontalScrollBarName = "Horizontal";  // TODO: Find out Spanish version
                    HorizontalScrollBar = "Horizontal ScrollBar";  // TODO: Find out Spanish version
                    VerticalScrollBar = "Vertical ScrollBar";  // TODO: Find out Spanish version
                    TableHorizontalScrollBar = "Horizontal Scroll Bar";  // TODO: Find out Spanish version
                    TableVerticalScrollBar = "Vertical Scroll Bar";  // TODO: Find out Spanish version
                    DataGridViewHeader = "Fila superior";
                    DataGridViewHeaderItemTopLeft = "Celda de encabezado superior izquierda";
                    break;
                default:
                    WinFormsUIA2VerticalScrollBarName = LocalizedStrings.WinFormsUIA2VerticalScrollBarName;
                    WinFormsUIA3VerticalScrollBarName = LocalizedStrings.WinFormsUIA3VerticalScrollBarName;
                    WinFormsUIA2HorizontalScrollBarName = LocalizedStrings.WinFormsUIA2HorizontalScrollBarName;
                    WinFormsUIA3HorizontalScrollBarName = LocalizedStrings.WinFormsUIA3HorizontalScrollBarName;
                    HorizontalScrollBar = LocalizedStrings.HorizontalScrollBar;
                    VerticalScrollBar = LocalizedStrings.VerticalScrollBar;
                    TableHorizontalScrollBar = LocalizedStrings.TableHorizontalScrollBar;
                    TableVerticalScrollBar = LocalizedStrings.TableVerticalScrollBar;
                    DataGridViewHeader = LocalizedStrings.DataGridViewHeader;
                    DataGridViewHeaderItemTopLeft = LocalizedStrings.DataGridViewHeaderItemTopLeft;
                    break;
            }
        }

        /// <summary>
        /// Name of a horizontal scrollbar.
        /// </summary>
        public static string HorizontalScrollBar { get; }

        /// <summary>
        /// Name of a vertical scrollbar.
        /// </summary>
        public static string VerticalScrollBar { get; }

        /// <summary>
        /// Name of a horizontal scrollbar in a table.
        /// </summary>
        public static string TableHorizontalScrollBar { get; }

        /// <summary>
        /// Name of a vertical scrollbar in a table.
        /// </summary>
        public static string TableVerticalScrollBar { get; }

        /// <summary>
        /// Name of a WinForms vertical scrollbar in UIA2.
        /// </summary>
        public static string WinFormsUIA2VerticalScrollBarName { get; }

        /// <summary>
        /// Name of a WinForms vertical scrollbar in UIA3.
        /// </summary>
        public static string WinFormsUIA3VerticalScrollBarName { get; }

        /// <summary>
        /// Name of a WinForms horizontal scrollbar in UIA2.
        /// </summary>
        public static string WinFormsUIA2HorizontalScrollBarName { get; }

        /// <summary>
        /// Name of a WinForms horizontal scrollbar in UIA3.
        /// </summary>
        public static string WinFormsUIA3HorizontalScrollBarName { get; }

        /// <summary>
        /// Name of the header row in a DataGridView.
        /// </summary>
        public static string DataGridViewHeader { get; set; }

        /// <summary>
        /// Name of the top-left header item.
        /// </summary>
        public static string DataGridViewHeaderItemTopLeft { get; set; }
    }
}
