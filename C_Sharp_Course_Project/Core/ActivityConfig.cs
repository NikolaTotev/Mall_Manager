using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core
{
    public class ActivityConfig
    {
        [JsonProperty]
        public List<string> Categories { get; set; }

        [JsonProperty]
        public List<Activity> Templates { get; set; }

        public ActivityConfig()
        {
            Categories = new List<string>();
            Templates = new List<Activity>();
        }
    }
}