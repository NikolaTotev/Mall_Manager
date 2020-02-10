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
using LiveCharts;
using LiveCharts.Wpf;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for RoomActivities.xaml
    /// </summary>
    public partial class RoomActivities : UserControl, IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private IAppView m_PreviousView;
        private Guid m_CurrentRoomID;
        private Room m_CurrentRoom;
        private StringBuilder sb;
        private List<ActivityListItem> activities;
        public RoomActivities(Guid currentRoomId, Room currentRoom)
        {
            InitializeComponent();
            sb = new StringBuilder();
            m_CurrentRoomID = currentRoomId;
            m_CurrentRoom = currentRoom;
            Lv_Activities.SelectionMode = SelectionMode.Multiple;
            activities = new List<ActivityListItem>();
            foreach (var activityId in m_CurrentRoom.Activities)
            {
                ActivityListItem itemToAdd = new ActivityListItem();
                Activity currentActivity = ActivityManager.GetInstance().Activities[activityId];
                itemToAdd.ActivityId = currentActivity.Id;
                itemToAdd.Description = currentActivity.Description;
                itemToAdd.Category = currentActivity.Category;
                itemToAdd.Status = currentActivity.CurActivityStatus;
                itemToAdd.SetStatusColor(currentActivity.CurActivityStatus);
                activities.Add(itemToAdd);
            }

            Lv_Activities.DataContext = activities;

            sb.Append(m_CurrentRoom.Name);
            sb.Append(" - activities");
            Lb_Header.Content = sb.ToString();
            RunStatistics();
        }

        private void RunStatistics()
        {
            SeriesCollection statistics = new SeriesCollection();
            
            PieSeries scheduled = new PieSeries();
            scheduled.LabelPoint = PieChartStatistics.PointLabel;
            scheduled.Title = "Scheduled";
            scheduled.Values = new ChartValues<int>{activities.Where(x => x.Status == ActivityStatus.Scheduled).Select(x => x).ToList().Count };
            scheduled.Fill = (Brush)Application.Current.Resources["ScheduledTask"];
            scheduled.DataLabels = true;
            statistics.Add(scheduled);

            PieSeries inProgress = new PieSeries();
            inProgress.LabelPoint = PieChartStatistics.PointLabel;
            inProgress.Title = "In Progress";
            inProgress.Values = new ChartValues<int> { activities.Where(x => x.Status == ActivityStatus.InProgress).Select(x => x).ToList().Count };
            inProgress.Fill = (Brush)Application.Current.Resources["InProgressTask"];
            inProgress.DataLabels = true;
            statistics.Add(inProgress);

            PieSeries finished = new PieSeries();
            finished.LabelPoint = PieChartStatistics.PointLabel;
            finished.Title = "Completed";
            finished.Values = new ChartValues<int>
                {activities.Where(x => x.Status == ActivityStatus.Finished).Select(x => x).ToList().Count};
            finished.Fill = (Brush)Application.Current.Resources["Completed"];
            finished.DataLabels = true;
            statistics.Add(finished);

            PieSeries failed = new PieSeries();
            failed.LabelPoint = PieChartStatistics.PointLabel;
            failed.Title = "Failed";
            failed.Values = new ChartValues<int> { activities.Where(x => x.Status == ActivityStatus.Failed).Select(x => x).ToList().Count };
            failed.Fill = (Brush)Application.Current.Resources["FailedTask"];
            failed.DataLabels = true;
            statistics.Add(failed);

            PieChartStatistics.UpdateData(statistics);
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddActivityMenu menuToDisplay = new AddActivityMenu(m_CurrentRoomID);
            m_CurrentMainWindow.ChangeView(menuToDisplay, this);
        }

        private void Btn_Stats_OnClick(object sender, RoutedEventArgs e)
        {
            StatisticsWindow win2 = new StatisticsWindow(m_CurrentRoomID);
            win2.Show();
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
            RunStatistics();
        }

        private void Btn_MarkAsFailed_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.Status = ActivityStatus.Failed;
                item.SetStatusColor(ActivityStatus.Failed);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Failed);
                Lv_Activities.Items.Refresh();
            }
            RunStatistics();
        }

        private void Btn_MarkAsCompleted_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.Status = ActivityStatus.Finished;
                item.SetStatusColor(ActivityStatus.Finished);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Finished);
                Lv_Activities.Items.Refresh();
            }
            RunStatistics();
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
                item.Status = ActivityStatus.InProgress;
                item.SetStatusColor(ActivityStatus.InProgress);
                ActivityManager.GetInstance().EditActivity(mallName:MallManager.GetInstance().CurrentMall.Name, activityId:item.ActivityId, status:ActivityStatus.InProgress);
                Lv_Activities.Items.Refresh();
            }
            RunStatistics();
        }

        private void Btn_MarkAsScheduled_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.Status = ActivityStatus.Scheduled;
                item.SetStatusColor(ActivityStatus.Scheduled);
                ActivityManager.GetInstance().EditActivity(mallName: MallManager.GetInstance().CurrentMall.Name, activityId: item.ActivityId, status: ActivityStatus.Scheduled);
                Lv_Activities.Items.Refresh();
            }
            RunStatistics();
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
