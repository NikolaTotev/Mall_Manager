using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProgramManager m_CurrentManager;
        public readonly ResourceDictionary Strings = new ResourceDictionary();
        
        public MainWindow()
        {
            InitializeComponent();
            m_CurrentManager = ProgramManager.GetInstance();
            Dashboard startDash = new Dashboard();
            ChangeView(startDash, null);
            Strings.Source= new Uri("pack://application:,,,/Resources/StringResources.xaml");
        }

        private void BigButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void ChangeView(IAppView viewToLoad, IAppView previousToSave)
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
    }
}
