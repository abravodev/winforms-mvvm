using System.Windows.Forms;

namespace WinformsTools.IntegrationTestUtils.Elements
{
    /// <summary>
    /// An exact copy of <see cref="DialogResult"/>, just to avoid namespace collisions when being used
    /// </summary>
    public enum DialogOption
    {
        //
        // Summary:
        //     Nothing is returned from the dialog box. This means that the modal dialog continues
        //     running.
        None = 0,
        //
        // Summary:
        //     The dialog box return value is OK (usually sent from a button labeled OK).
        OK = 1,
        //
        // Summary:
        //     The dialog box return value is Cancel (usually sent from a button labeled Cancel).
        Cancel = 2,
        //
        // Summary:
        //     The dialog box return value is Abort (usually sent from a button labeled Abort).
        Abort = 3,
        //
        // Summary:
        //     The dialog box return value is Retry (usually sent from a button labeled Retry).
        Retry = 4,
        //
        // Summary:
        //     The dialog box return value is Ignore (usually sent from a button labeled Ignore).
        Ignore = 5,
        //
        // Summary:
        //     The dialog box return value is Yes (usually sent from a button labeled Yes).
        Yes = 6,
        //
        // Summary:
        //     The dialog box return value is No (usually sent from a button labeled No).
        No = 7
    }
}
