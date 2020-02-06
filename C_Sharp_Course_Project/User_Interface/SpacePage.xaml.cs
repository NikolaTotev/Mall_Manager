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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SpacePage : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private readonly Guid m_CurrentRoomId;
        private readonly Room m_CurrentRoom;
        public SpacePage(Guid currentRoomId)
        {
            InitializeComponent();
            m_CurrentRoomId = currentRoomId;
            m_CurrentRoom = RoomManager.GetInstance().Rooms[currentRoomId];
            Lb_RoomName.Content = m_CurrentRoom.Name;
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_Activities_OnClick(object sender, RoutedEventArgs e)
        {
            RoomActivities activitiesPageToLoad = new RoomActivities(m_CurrentRoomId, m_CurrentRoom);
            activitiesPageToLoad.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeView(activitiesPageToLoad,this);
        }

        private void Btn_SpaceInfo_OnClick(object sender, RoutedEventArgs e)
        {
            RoomInfoPage infoPageToLoad = new RoomInfoPage(m_CurrentRoom);
            infoPageToLoad.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeView(infoPageToLoad, this);
        }
    }
}
