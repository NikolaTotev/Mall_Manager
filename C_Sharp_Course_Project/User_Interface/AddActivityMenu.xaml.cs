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
        private string m_CurrentCategory;
        private bool m_ValidDate;
        public AddActivityMenu(Guid currentRoomId)
        {
            InitializeComponent();
            m_CurrentRoomID = currentRoomId;
            if (ActivityManager.GetInstance().GetCategories() != null)
            {
                foreach (var category in ActivityManager.GetInstance().GetCategories())
                {
                    Cmb_ActivityCat.Items.Add(category);
                }
            }
            else
            {
                Cmb_ActivityCat.Items.Add("Other");
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

      public void Validate()
        {
            if (Tb_Desc != null && Btn_Add != null)
            {
                bool descOk = Tb_Desc.Text != "Activity Description" && !string.IsNullOrEmpty(Tb_Desc.Text);
                bool catOk = !string.IsNullOrEmpty(m_CurrentCategory);
                if (descOk && catOk && m_ValidDate)
                {
                    Btn_Add.IsEnabled = true;

                    Lb_DescError.Content = "";
                    Lb_ActivityCatError.Content = "";
                }
                else
                {
                    Btn_Add.IsEnabled = false;

                    Lb_DescError.Content = !descOk ? m_CurrentMainWindow.Strings["InvalidActivityDesc"] : "";
                    Lb_ActivityCatError.Content = !catOk ? m_CurrentMainWindow.Strings["InvalidActivityCategory"] : "";
                }
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            string descText = Tb_Desc.Text;
            Activity activityToAdd = new Activity(Guid.NewGuid(), m_CurrentRoomID, m_CurrentCategory, false, descText, ActivityStatus.Scheduled, Cal_StartDate.DisplayDate, Cal_EndDate.DisplayDate);
            ActivityManager.GetInstance().AddActivity(activityToAdd, MallManager.GetInstance().CurrentMall.Name);
            if (m_CurrentRoomID == MallManager.GetInstance().CurrentMall.Id)
            {
                MallActivities mallActivitiesPage = new MallActivities();
                m_CurrentMainWindow.ChangeView(mallActivitiesPage, this);
            }
            else
            {
                RoomActivities newActivitiesPage =
                    new RoomActivities(m_CurrentRoomID, RoomManager.GetInstance().Rooms[m_CurrentRoomID]);
                m_CurrentMainWindow.ChangeView(newActivitiesPage, this);
            }
        }

        private void Cmb_ActivityCat_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_CurrentCategory = Cmb_ActivityCat.SelectedItem.ToString();
        }

        private void Cal_StartDate_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateDate();
        }

        private void Cal_EndDate_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateDate();
        }
        
        private void ValidateDate()
        {
            if (Cal_StartDate.SelectedDate <= Cal_EndDate.SelectedDate)
            {
                m_ValidDate = true;
                Lb_DateError.Content = "";
                Validate();
            }
            else
            {
                m_ValidDate = false;
                Lb_DateError.Content = "Date is invalid!";
                Validate();
            }
        }

       
    }
}
