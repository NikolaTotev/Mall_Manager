using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Core;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;


namespace User_Interface
{
    public static class VisualizationPreProcessor
    {
        public static SeriesCollection GenerateBasicActivityColumnGraphics(IList<(string Title, List<int> Values, ActivityStatus Status)> data)
        {
            SeriesCollection mySeries = new SeriesCollection();
            if (data != null)
            {
                foreach (var (title, value, status) in data)
                {
                    mySeries.Add(new ColumnSeries
                    { Title = title, Values = new ChartValues<int> (value), Fill = GetChartColor(status)});
                }
            }

            return mySeries;
        }

        public static SeriesCollection GenerateBasicActivityPieGraphics(IList<(string Title, List<int> Value, ActivityStatus Status)> data)
        {
            SeriesCollection mySeries = new SeriesCollection();
            if (data != null)
            {
                foreach (var (title, values, status) in data)
                {
                    mySeries.Add(new PieSeries
                    {
                        Title = title,
                        Values = new ChartValues<int> {values.Sum()},
                        Fill = GetChartColor(status),
                        DataLabels = true
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

        public static IList<(string Title, List<int> Values, ActivityStatus Status)> GetRoomActivityInfoData(Guid roomId)
        {
            List<Guid> activityIds = RoomManager.GetInstance().Rooms[roomId].Activities;
            List<Activity> activities = new List<Activity>();
            foreach (var activity in activityIds)
            {
                activities.Add(ActivityManager.GetInstance().Activities[activity]);
            }

            List<int> scheduled = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count
            };
            List<int> inProgress = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count
            };
            List<int> completed = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count
            };
            List<int> failed = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count
            };

            var res = new List<(string Title, List<int> Values, ActivityStatus Status)>
            {
                (Title: "Scheduled", Values: scheduled, Status: ActivityStatus.Scheduled),
                (Title: "In Progress", Values: inProgress,Status: ActivityStatus.InProgress),
                (Title: "Completed", Values: completed,Status: ActivityStatus.Finished),
                (Title: "Failed", Values: failed,Status: ActivityStatus.Failed)
            };

            return res;
        }

        public static (List<string> rooms, ChartValues<HeatPoint>)? GetMallActivityInfoData()
        {
            List<Activity> activities = ActivityManager.GetInstance().Activities.Values.ToList();
            List<Room> rooms = RoomManager.GetInstance().Rooms.Values.ToList();
            Mall currentMall = MallManager.GetInstance().CurrentMall;

            List<string> roomNames = new List<string>();
            ChartValues<HeatPoint> points = new ChartValues<HeatPoint>();

            //Mall Activities
            roomNames.Add(currentMall.Name);
            int counter = 0;
            points.Add(new HeatPoint(0,counter,
                activities.Where(a => a.CorrespondingRoom == currentMall.Id).Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count));
            points.Add(new HeatPoint(1, counter, 
                activities.Where(a => a.CorrespondingRoom == currentMall.Id).Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count));
            points.Add(new HeatPoint(2, counter, 
                activities.Where(a => a.CorrespondingRoom == currentMall.Id).Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count));
            points.Add(new HeatPoint(3, counter, 
                activities.Where(a => a.CorrespondingRoom == currentMall.Id).Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count));
            counter++;

            //Rooms Activities
            foreach (var room in rooms)
            {
                roomNames.Add(room.Name);
                points.Add(new HeatPoint(0, counter,
                    activities.Where(a => a.CorrespondingRoom == room.Id).Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count));
                points.Add(new HeatPoint(1, counter,
                    activities.Where(a => a.CorrespondingRoom == room.Id).Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count));
                points.Add(new HeatPoint(2, counter,
                    activities.Where(a => a.CorrespondingRoom == room.Id).Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count));
                points.Add(new HeatPoint(3, counter,
                    activities.Where(a => a.CorrespondingRoom == room.Id).Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count));
                counter++;

            }
            return (roomNames, points);
        }

        public static IList<(string Title, List<int> Value, ActivityStatus Status)> GetMallAssociatedActivityInfoData(Guid mallId)
        {
            List<Guid> activityIds = MallManager.GetInstance().Malls[mallId].AssociatedActivities;
            List<Activity> activities = new List<Activity>();
            foreach (var activity in activityIds)
            {
                activities.Add(ActivityManager.GetInstance().Activities[activity]);
            }

            List<int> scheduled = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList().Count
            };
            List<int> inProgress = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList().Count
            };
            List<int> completed = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList().Count
            };
            List<int> failed = new List<int>
            {
                activities.Where(a => a.Category == "Cleaning")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count,
                activities.Where(a => a.Category == "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count,
                activities.Where(a => a.Category != "Cleaning" && a.Category != "Maintenance")
                    .Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList().Count
            };

            var res = new List<(string Title, List<int> Value, ActivityStatus Status)>
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
