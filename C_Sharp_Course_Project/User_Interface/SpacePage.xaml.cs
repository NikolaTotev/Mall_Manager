using System;
using System.Windows;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class SpacePage : IAppView
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
            Tbl_HeaderText.Text= m_CurrentRoom.Name;
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousView)
        {
            m_PreviousView = previousView;
        }

        public void SetNextView(IAppView nextView)
        {
            //Implement as needed
        }

        public IAppView GetPreviousView()
        {
            return m_PreviousView;
        }

        private void Btn_Activities_OnClick(object sender, RoutedEventArgs e)
        {
            RoomActivities activitiesPageToLoad = new RoomActivities(m_CurrentRoomId, m_CurrentRoom);
            activitiesPageToLoad.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeViewForward(activitiesPageToLoad, this);
        }

        private void Btn_SpaceInfo_OnClick(object sender, RoutedEventArgs e)
        {
            RoomInfoPage infoPageToLoad = new RoomInfoPage(m_CurrentRoom);
            infoPageToLoad.SetMainWindow(m_CurrentMainWindow);
            m_CurrentMainWindow.ChangeViewForward(infoPageToLoad, this);
        }

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeViewBackward(m_PreviousView, this);
        }
    }
}
