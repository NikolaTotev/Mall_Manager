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
    /// Interaction logic for MallActivities.xaml
    /// </summary>
    public partial class MallActivities : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private StringBuilder sb;
        private List<ActivityListItem> activities;
        public MallActivities()
        {
            InitializeComponent();
            sb = new StringBuilder();
            Lv_Activities.SelectionMode = SelectionMode.Multiple;
            activities = new List<ActivityListItem>();
            foreach (var activityId in MallManager.GetInstance().CurrentMall.AssociatedActivities)
            {
                ActivityListItem itemToAdd = new ActivityListItem();
                Activity currentActivity = ActivityManager.GetInstance().Activities[activityId];
                itemToAdd.ActivityId = currentActivity.Id;
                itemToAdd.Description = currentActivity.Description;
                itemToAdd.Category = currentActivity.Category;
                itemToAdd.SetStatusColor(currentActivity.CurActivityStatus);
                activities.Add(itemToAdd);
            }

            DataContext = activities;

            sb.Append(MallManager.GetInstance().CurrentMall.Name);
            sb.Append(" - activities");
            Lbl_MallName.Content = sb.ToString();
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddActivityMenu menuToDisplay = new AddActivityMenu(MallManager.GetInstance().CurrentMall.Id);
            m_CurrentMainWindow.ChangeView(menuToDisplay, this);
        }

        private void Btn_Stats_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Btn_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem itemToDelete = (ActivityListItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().DeleteActivity(itemToDelete.ActivityId, MallManager.GetInstance().CurrentMall.Name);
                activities.Remove(itemToDelete);
                Lv_Activities.Items.Refresh();
            }
        }

        private void Btn_MarkAsFailed_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Failed);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Failed);
                Lv_Activities.Items.Refresh();
            }
        }

        private void Btn_MarkAsCompleted_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Finished);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Finished);
                Lv_Activities.Items.Refresh();
            }
        }

        private void Btn_Deselect_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (ActivityListItem item in Lv_Activities.Items)
            {
                item.IsSelected = false;
            }
        }

        private void Btn_MarkAsInProgress_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.InProgress);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.InProgress);
                Lv_Activities.Items.Refresh();
            }
        }

        private void Btn_MarkAsScheduled_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Scheduled);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Scheduled);
                Lv_Activities.Items.Refresh();
            }
        }

        private void Btn_SelectAll_OnClick(object sender, RoutedEventArgs e)
        {
            Lv_Activities.SelectAll();
        }

        public void SetMainWindow(MainWindow currentWindow)
        {
            m_CurrentMainWindow = currentWindow;
        }

        public void SetPreviousView(IAppView previousElement)
        {
            m_PreviousView = previousElement;
        }

        private void Lv_Activities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
