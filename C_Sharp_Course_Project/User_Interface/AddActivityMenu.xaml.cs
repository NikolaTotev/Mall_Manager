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
    /// Interaction logic for AddActivityMenu.xaml
    /// </summary>
    public partial class AddActivityMenu : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private Guid m_CurrentRoomID;
        public AddActivityMenu(Guid currentRoomId)
        {
            InitializeComponent();
            m_CurrentRoomID = currentRoomId;
        }

        private void Btn_Save_OnClick(object sender, RoutedEventArgs e)
        {
            string descText = Tb_Desc.Text;
            string catText = Tb_Category.Text;
            Activity activityToAdd = new Activity(Guid.NewGuid(), m_CurrentRoomID, catText, false, descText, ActivityStatus.Scheduled, DateTime.Now, new DateTime(2020, 12, 30));
            ActivityManager.GetInstance().AddActivity(activityToAdd, MallManager.GetInstance().CurrentMall.Name);
            RoomActivities newActivitiesPage = new RoomActivities(m_CurrentRoomID, RoomManager.GetInstance().Rooms[m_CurrentRoomID]);
            m_CurrentMainWindow.ChangeView(newActivitiesPage, this);
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Btn_Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            m_CurrentMainWindow.ChangeView(m_PreviousView, this);
        }

        private void Tb_Category_GotFocus(object sender, RoutedEventArgs e)
        {
            Tb_Category.Text = "";
        }

        private void Tb_Desc_GotFocus(object sender, RoutedEventArgs e)
        {
            Tb_Desc.Text = "";
        }
    }
}
