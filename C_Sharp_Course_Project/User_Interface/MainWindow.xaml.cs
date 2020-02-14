using System;
using System.Windows;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public readonly ResourceDictionary Strings = new ResourceDictionary();
        
        public MainWindow()
        {
            InitializeComponent();
            ProgramManager.GetInstance();
            Dashboard startDash = new Dashboard();
            ChangeViewForward(startDash, null);
            Strings.Source= new Uri("pack://application:,,,/Resources/StringResources.xaml");
        }
        
        public void ChangeViewForward(IAppView viewToLoad, IAppView previousToSave)
        {
            if (viewToLoad != null)
            {
                p_StartPanel.Children.Clear();
                viewToLoad.SetMainWindow(this);
                viewToLoad.SetPreviousView(previousToSave);
                if (viewToLoad is UIElement elementToAdd)
                {
                    p_StartPanel.Children.Add(elementToAdd);
                }
            }
        }

        public void ChangeViewBackward(IAppView viewToLoad, IAppView nextView)
        {
            if (viewToLoad != null)
            {
                p_StartPanel.Children.Clear();
                viewToLoad.SetMainWindow(this);
                viewToLoad.SetNextView(nextView);
                if (viewToLoad is UIElement elementToAdd)
                {
                    p_StartPanel.Children.Add(elementToAdd);
                }
            }
        }

        public void ReturnFromAddMenu(IAppView viewToReturnTo, IAppView previousViewValueToRestore)
        {
            if (viewToReturnTo != null)
            {
                p_StartPanel.Children.Clear();
                viewToReturnTo.SetMainWindow(this);
                viewToReturnTo.SetPreviousView(previousViewValueToRestore);
                if (viewToReturnTo is UIElement elementToAdd)
                {
                    p_StartPanel.Children.Add(elementToAdd);
                }
            }
        }

        public void AddOverlay(UIElement overlay)
        {
            p_StartPanel.Children.Add(overlay);
        }

        public void RemoveOverlay(UIElement overlay)
        {
            p_StartPanel.Children.Remove(overlay);
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
