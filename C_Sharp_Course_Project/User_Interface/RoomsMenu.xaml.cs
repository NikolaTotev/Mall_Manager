using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    /// Interaction logic for RoomsMenu.xaml
    /// </summary>
    public partial class RoomsMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        public RoomsMenu()
        {
            InitializeComponent();
            Lbl_MallName.Content = MallManager.GetInstance().CurrentMall.Name;
            LoadRentalSpaces();
        }

        public void LoadRentalSpaces()
        {
            Lv_RentalSpaces.Items.Clear();
            foreach (var rentalSpce in RoomManager.GetInstance().GetRooms())
            {
                Lv_RentalSpaces.Items.Add(rentalSpce);
            }
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_AddRentalSpace_Click(object sender, RoutedEventArgs e)
        {
            AddRoomMenu addRoom = new AddRoomMenu();
            m_CurrentMainWindow.ChangeView(addRoom, this);
        }
    }
}
