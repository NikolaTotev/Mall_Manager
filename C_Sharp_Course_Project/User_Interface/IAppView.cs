using System.Windows;

namespace User_Interface
{
    public interface IAppView
    {
        void SetMainWindow(MainWindow currentWindow);
        void SetPreviousView(IAppView previousElement);
    }
}