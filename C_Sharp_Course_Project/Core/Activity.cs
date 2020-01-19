using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Activity
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public int CorrespondingRoom { get; set; }
        public ActivityStatus CurActivityStatus { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public bool IsTemplate { get; set; }

        public Activity(string category = " ", bool isTemplate = false, string description = " ", int corespRoom = -1, ActivityStatus status = ActivityStatus.Undefined, DateTime scheduleDate = new DateTime())
        {
            IsTemplate = isTemplate;
            Category = category;
            Description = description;
            CorrespondingRoom = corespRoom;
            CurActivityStatus = status;
            ScheduleDateTime = scheduleDate;
        }
    }
}
