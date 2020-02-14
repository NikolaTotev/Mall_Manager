using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core;

namespace User_Interface
{
    /// <summary>
    /// Interaction logic for RoomActivities.xaml
    /// </summary>
    public partial class RoomActivities : IAppView
    {
        private MainWindow m_CurrentMainWindow;
        private StatisticsWindow m_StatWinow;

        private IAppView m_PreviousView;

        private readonly Guid m_CurrentRoomId;
        private readonly List<ActivityListItem> m_Activities;
        
        public RoomActivities(Guid currentRoomId, Room currentRoom)
        {
            InitializeComponent();
            var sb = new StringBuilder();
            m_CurrentRoomId = currentRoomId;
            var currentRoom1 = currentRoom;

            Lv_Activities.SelectionMode = SelectionMode.Multiple;
            m_Activities = new List<ActivityListItem>();
            foreach (var activityId in currentRoom1.Activities)
            {
                ActivityListItem itemToAdd = new ActivityListItem();
                Activity currentActivity = ActivityManager.GetInstance().Activities[activityId];
                itemToAdd.ActivityId = currentActivity.Id;
                itemToAdd.Description = currentActivity.Description;
                itemToAdd.Category = currentActivity.Category;
                itemToAdd.Status = currentActivity.CurActivityStatus;
                itemToAdd.SetStatusColor(currentActivity.CurActivityStatus);
                m_Activities.Add(itemToAdd);
            }

            DataContext = m_Activities;

            sb.Append(currentRoom1.Name);
            sb.Append(" - Activities");
            Tbl_HeaderText.Text = sb.ToString();

            LoadQuickStats();
        }

        private void LoadQuickStats()
        {
            BackgroundWorker initWorker = new BackgroundWorker { WorkerReportsProgress = true };

            initWorker.DoWork += (worker, args) =>
            {
                if (!args.Cancel) args.Result = VisualizationPreProcessor.GetRoomActivityInfoData(m_CurrentRoomId);
            };

            initWorker.ProgressChanged += (o, args) => { };
            initWorker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Error != null)
                {
                    Lb_StatsLoading.Content = "Error Loading Statistics.";
                    Lb_StatsLoading.Foreground = Brushes.IndianRed;
                }
                else if (args.Cancelled)
                {
                    Lb_StatsLoading.Content = "Error Loading Statistics.";
                    Lb_StatsLoading.Foreground = Brushes.IndianRed;
                }
                else
                {
                    if (args.Result is IList<(string Title, int Value, ActivityStatus Status)>)
                    {
                        Sp_QuickStats.Children.Remove(Lb_StatsLoading);
                        PieChartStatistics.UpdateData(VisualizationPreProcessor.GenerateBasicActivityPieGraphics(args.Result as IList<(string Title, int Value, ActivityStatus Status)>, PieChartStatistics.PointLabel));
                    }
                }
            };

            if (!initWorker.IsBusy)
            {
                initWorker.RunWorkerAsync();
            }
        }

        private void Btn_Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddActivityMenu menuToDisplay = new AddActivityMenu(m_CurrentRoomId);
            m_CurrentMainWindow.ChangeViewForward(menuToDisplay, this);
        }
        
        private void Btn_Delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem itemToDelete = (ActivityListItem)Lv_Activities.SelectedItems[i];
                ActivityManager.GetInstance().DeleteActivity(itemToDelete.ActivityId);
                m_Activities.Remove(itemToDelete);
                Lv_Activities.Items.Refresh();
            }
            LoadQuickStats();
        }

        private void Btn_Stats_OnClick(object sender, RoutedEventArgs e)
        {
            m_StatWinow= new StatisticsWindow(m_CurrentRoomId, false);
            m_StatWinow.Show();
        }

        private void Btn_MarkAsScheduled_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Scheduled);
                ActivityManager.GetInstance().EditActivityStatus(activityId: item.ActivityId, status: ActivityStatus.Scheduled);
                Lv_Activities.Items.Refresh();
            }
            LoadQuickStats();
        }


        private void Btn_MarkAsInProgress_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.Status = ActivityStatus.InProgress;
                item.SetStatusColor(ActivityStatus.InProgress);
                ActivityManager.GetInstance().EditActivityStatus(activityId: item.ActivityId, status: ActivityStatus.InProgress);
                Lv_Activities.Items.Refresh();
            }
            LoadQuickStats();
        }


        private void Btn_MarkAsCompleted_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Finished);
                ActivityManager.GetInstance().EditActivityStatus(activityId: item.ActivityId, status: ActivityStatus.Finished);
                Lv_Activities.Items.Refresh();
            }
            LoadQuickStats();
        }

        private void Btn_MarkAsFailed_OnClick(object sender, RoutedEventArgs e)
        {
            if (Lv_Activities.SelectedItems.Count <= 0) return;
            for (var i = Lv_Activities.SelectedItems.Count - 1; i >= 0; i--)
            {
                ActivityListItem item = (ActivityListItem)Lv_Activities.SelectedItems[i];
                item.SetStatusColor(ActivityStatus.Failed);
                ActivityManager.GetInstance().EditActivityStatus(activityId: item.ActivityId, status: ActivityStatus.Failed);
                Lv_Activities.Items.Refresh();
            }
            LoadQuickStats();
        }

        private void Btn_SelectAll_OnClick(object sender, RoutedEventArgs e)
        {
            Lv_Activities.SelectAll();
        }

        private void Btn_Deselect_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (ActivityListItem item in Lv_Activities.Items)
            {
                item.IsSelected = false;
            }
        }

        #region View Controls

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

        private void Btn_Back_OnClick(object sender, RoutedEventArgs e)
        {
            m_StatWinow?.Close();
            m_CurrentMainWindow.ChangeViewBackward(m_PreviousView, this);
        }
        #endregion
    }
}
