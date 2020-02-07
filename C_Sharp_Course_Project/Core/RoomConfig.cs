using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core
{
    public class RoomConfig
    {
        [JsonProperty]
        public List<string> RoomTypes { get; set; }

        public RoomConfig()
        {
            RoomTypes = new List<string>();
        }
    }
}
