using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Core;
using LiveCharts;
using LiveCharts.Wpf;


namespace User_Interface
{
    public static class VisualizationPreProcessor
    {
        public static SeriesCollection GenerateBasicActivityColumnGraphics(IList<(string Title, int Value, ActivityStatus Status)> data)
        {
            SeriesCollection mySeries = new SeriesCollection();
            if (data != null)
            {
                foreach (var (title, value, status) in data)
                {
                    mySeries.Add(new ColumnSeries
                    { Title = title, Values = new ChartValues<int> { value }, Fill = GetChartColor(status) });
                }
            }

            return mySeries;
        }

        public static SeriesCollection GenerateBasicActivityPieGraphics(IList<(string Title, int Value, ActivityStatus Status)> data, Func<ChartPoint, string> lbPoint)
        {
            SeriesCollection mySeries = new SeriesCollection();
            if (data != null)
            {
                foreach (var (title, value, status) in data)
                {
                    mySeries.Add(new PieSeries
                    {
                        Title = title,
                        Values = new ChartValues<int> { value },
                        Fill = GetChartColor(status),
                        DataLabels = true,
                        LabelPoint = lbPoint
                    });
                }
            }

            return mySeries;
        }

        private static Brush GetChartColor(ActivityStatus input)
        {
            switch (input)
            {
                case ActivityStatus.Scheduled:
                    return (Brush)Application.Current.Resources["ScheduledTask"];
                case ActivityStatus.InProgress:
                    return (Brush)Application.Current.Resources["InProgressTask"];
                case ActivityStatus.Finished:
                    return (Brush)Application.Current.Resources["Completed"];
                case ActivityStatus.Failed:
                    return (Brush)Application.Current.Resources["FailedTask"];
                case ActivityStatus.Undefined:
                    return (Brush)Application.Current.Resources["UndefinedTask"];
                default:
                    return Brushes.RoyalBlue;
            }
        }

        public static IList<(string Title, int Value, ActivityStatus Status)> GetRoomActivityInfoData(Guid roomId)
        {
            List<Guid> activityIds = RoomManager.GetInstance().Rooms[roomId].Activities;
            List<Activity> activities = new List<Activity>();
            foreach (var activity in activityIds)
            {
                activities.Add(ActivityManager.GetInstance().Activities[activity]);
            }

            var scheduled = activities.Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count;
            var inProgress = activities.Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count;
            var completed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count;
            var failed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count;

            var res = new List<(string Title, int Value, ActivityStatus Status)>
            {
                (Title: "Scheduled", Value: scheduled, Status: ActivityStatus.Scheduled),
                (Title: "In Progress", Value: inProgress,Status: ActivityStatus.InProgress),
                (Title: "Completed", Value: completed,Status: ActivityStatus.Finished),
                (Title: "Failed", Value: failed,Status: ActivityStatus.Failed)
            };

            return res;
        }

        public static IList<(string Title, int Value, ActivityStatus Status)> GetMallActivityInfoData()
        {
            List<Guid> mallActivities = MallManager.GetInstance().CurrentMall.AssociatedActivities;

            List<Activity> activities = new List<Activity>();
            foreach (var mallActivity in mallActivities)
            {
                activities.Add(ActivityManager.GetInstance().Activities[mallActivity]);
            }
            
            var scheduled = activities.Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count;
            var inProgress = activities.Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count;
            var completed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count;
            var failed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count;

            var res = new List<(string Title, int Value, ActivityStatus Status)>
            {
                (Title: "Scheduled", Value: scheduled, Status: ActivityStatus.Scheduled),
                (Title: "In Progress", Value: inProgress,Status: ActivityStatus.InProgress),
                (Title: "Completed", Value: completed,Status: ActivityStatus.Finished),
                (Title: "Failed", Value: failed,Status: ActivityStatus.Failed)
            };

            return res;
        }

        public static IList<(string Title, int Value, ActivityStatus Status)> GetMallAssociatedActivityInfoData(Guid mallId)
        {
            List<Guid> activityIds = MallManager.GetInstance().Malls[mallId].AssociatedActivities;
            List<Activity> activities = new List<Activity>();
            foreach (var activity in activityIds)
            {
                activities.Add(ActivityManager.GetInstance().Activities[activity]);
            }

            var scheduled = activities.Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count;
            var inProgress = activities.Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count;
            var completed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count;
            var failed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count;

            var res = new List<(string Title, int Value, ActivityStatus Status)>
            {
                (Title: "Scheduled", Value: scheduled, Status: ActivityStatus.Scheduled),
                (Title: "In Progress", Value: inProgress, Status: ActivityStatus.InProgress),
                (Title: "Completed", Value: completed, Status: ActivityStatus.Finished),
                (Title: "Failed", Value: failed, Status: ActivityStatus.Failed)
            };

            return res;
        }
    }
}
