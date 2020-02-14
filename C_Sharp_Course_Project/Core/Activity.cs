using System;
using Newtonsoft.Json;

namespace Core
{
    public class Activity
    {
        [JsonProperty] public readonly Guid Id;
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid CorrespondingRoom { get; set; }
        public ActivityStatus CurActivityStatus { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsTemplate { get; set; }

        public Activity(Guid newId, Guid corespRoom, string category = " ", bool isTemplate = false,
            string description = " ", ActivityStatus status = ActivityStatus.Undefined,
            DateTime startDate = default, DateTime endDate = default)
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