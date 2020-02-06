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


        private void Tb_Desc_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_Desc.Text == "Activity Description")
            {
                Tb_Desc.Text = "";
            }
        }

        private void Tb_Desc_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_Desc.Text))
            {
                Tb_Desc.Text = "Activity Description";
            }
        }

        private void Tb_Desc_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        private void Tb_Category_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (Tb_Category.Text == "Activity Category")
            {
                Tb_Category.Text = "";
            }
        }

        private void Tb_Category_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_Category.Text))
            {
                Tb_Category.Text = "Activity Category";
            }
        }

        private void Tb_Category_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Validate();
        }

        public void Validate()
        {
            if (Tb_Desc != null && Tb_Category != null && Btn_Add != null)
            {
                bool descOk = Tb_Desc.Text != "Activity Description" && !string.IsNullOrEmpty(Tb_Desc.Text);
                bool catOk = Tb_Category.Text != "Activity Category" && !string.IsNullOrEmpty(Tb_Category.Text);
                if (descOk && catOk)
                {
                    Btn_Add.IsEnabled = true;

                    Lb_DescError.Content = "";
                    Lb_CategoryError.Content = "";
                }
                else
                {
                    Btn_Add.IsEnabled = false;

                    Lb_DescError.Content = !descOk ? m_CurrentMainWindow.Strings["InvalidActivityDesc"] : "";
                    Lb_CategoryError.Content = !catOk ? m_CurrentMainWindow.Strings["InvalidActivityCategory"] : "";
                }
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            string descText = Tb_Desc.Text;
            string catText = Tb_Category.Text;
            Activity activityToAdd = new Activity(Guid.NewGuid(), m_CurrentRoomID, catText, false, descText, ActivityStatus.Scheduled, DateTime.Now, new DateTime(2020, 12, 30));
            ActivityManager.GetInstance().AddActivity(activityToAdd, MallManager.GetInstance().CurrentMall.Name);
            RoomActivities newActivitiesPage = new RoomActivities(m_CurrentRoomID, RoomManager.GetInstance().Rooms[m_CurrentRoomID]);
            m_CurrentMainWindow.ChangeView(newActivitiesPage, this);
        }
    }
}
