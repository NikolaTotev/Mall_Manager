using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core
{
    public class Activity
    {
        [JsonProperty]
        public readonly string Id;
        public string Category { get; set; }
        public string Description { get; set; }
        public string CorrespondingRoom { get; set; }
        public ActivityStatus CurActivityStatus { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsTemplate { get; set; }

        public Activity(string newId = " ", string category = " ", bool isTemplate = false, string description = " ", string corespRoom = " ", ActivityStatus status = ActivityStatus.Undefined,DateTime startDate = default(DateTime) ,DateTime endDate = default(DateTime))
        {
            Id = newId;
            IsTemplate = isTemplate;
            Category = category;
            Description = description;
            CorrespondingRoom = corespRoom;
            CurActivityStatus = status;
            StartTime = startDate;
            EndTime = endDate;
        }
    }
}
