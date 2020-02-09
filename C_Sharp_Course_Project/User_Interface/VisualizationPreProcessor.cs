using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using LiveCharts;
using LiveCharts.Wpf;

namespace User_Interface
{
    public static class VisualizationPreProcessor
    {
        public static SeriesCollection BasicActivityInfo(Guid roomId)
        {
            List<Guid> activityIds = RoomManager.GetInstance().Rooms[roomId].Activities;
            List<Activity> activities = new List<Activity>();
            foreach (var activity in activityIds)
            {
                activities.Add(ActivityManager.GetInstance().Activities[activity]);
            }

            var scheduled = activities.Where(a => a.CurActivityStatus is ActivityStatus.Scheduled).ToList();
            var inProgress = activities.Where(a => a.CurActivityStatus is ActivityStatus.InProgress).ToList();
            var completed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Finished).ToList();
            var failed = activities.Where(a => a.CurActivityStatus is ActivityStatus.Failed).ToList();

            SeriesCollection mySeries = new SeriesCollection();
            mySeries.Add(new ColumnSeries { Title = "Scheduled", Values = new ChartValues<int> { scheduled.Count } });
            mySeries.Add(new ColumnSeries { Title = "In Progress", Values = new ChartValues<int> { inProgress.Count } });
            mySeries.Add(new ColumnSeries { Title = "Completed", Values = new ChartValues<int> { completed.Count } });
            mySeries.Add(new ColumnSeries { Title = "Failed", Values = new ChartValues<int> { failed.Count } });

            return mySeries;
        }
    }
}
