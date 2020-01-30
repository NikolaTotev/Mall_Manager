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
    /// Interaction logic for AddRoomMenu.xaml
    /// </summary>
    public partial class AddRoomMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousElement;
        public AddRoomMenu()
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeView(m_PreviousElement, this);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RoomManager.GetInstance().CreateRoom(txtBoxName.Text, txtBoxDesc.Text, txtBoxType.Text,
                int.Parse(txtBoxFloor.Text), int.Parse(txtBoxNumber.Text), Guid.NewGuid(),
                MallManager.GetInstance().CurrentMall.Name);
            RoomsMenu menu = new RoomsMenu();
            m_CurrentMainWindow.ChangeView(menu, this);
        }
    }
}
