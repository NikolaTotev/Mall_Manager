using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Activity
    {
        public string Category { get; set; }
        public string Description { get; set; }

        public int UsingTemplate { get; set; }
        public int CorrespondingRoom { get; set; }

        public ActivityStatus CurActivityStatus { get; set; }
        public DateTime ScheduleDateTime { get; set; }

        //TODO Add methods for creating activity.
    }
}
