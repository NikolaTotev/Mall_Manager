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
    /// Interaction logic for AddMallMenu.xaml
    /// </summary>
    public partial class AddMallMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousElement;
        public AddMallMenu()
        {
            InitializeComponent();
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousElement = previousElement;
        }

        private void CancelButton_Onclick(object sender, RoutedEventArgs e)
        {
            
            m_CurrentMainWindow.ChangeView(m_PreviousElement,this);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Mall mallToAdd = new Mall(Guid.NewGuid(), TxtB_MallName.Text, TxtB_MallDesc.Text);
            MallManager.GetInstance().AddMall(mallToAdd);
            Dashboard newDash = new Dashboard();
            newDash.SetPreviousView(this);
            newDash.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeView(newDash, this);
        }
    }
}
