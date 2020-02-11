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
    /// Interaction logic for Mall.xaml
    /// </summary>
    public partial class MallMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        public MallMenu()
        {
            InitializeComponent();
            Tbl_MallName.Text = MallManager.GetInstance().CurrentMall.Name;
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_RentalSpaces_Click(object sender, RoutedEventArgs e)
        {
            RoomsMenu rooms = new RoomsMenu();
            m_CurrentMainWindow.ChangeView(rooms, this);
        }

        private void Btn_Statistics_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_ActivityOverview_OnClick(object sender, RoutedEventArgs e)
        {
           MallActivities mallActivities = new MallActivities();
           m_CurrentMainWindow.ChangeView(mallActivities, this);
        }

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeView(m_PreviousView, this);
        }
    }
}
