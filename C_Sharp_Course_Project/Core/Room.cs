using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core
{
    public class Room
    {
        [JsonProperty]
        public readonly Guid Id;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public List<Guid> Activities { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastEditDate { get; private set; }

        public Room(string name, string description, string type, int roomNumber, int floorNumber, Guid newId)
        {
            Name = name;
            Description = description;
            Type = type;
            Floor = floorNumber;
            RoomNumber = roomNumber;
            Id = newId;
            CreateDate = DateTime.UtcNow;
            LastEditDate = DateTime.UtcNow;
            Activities = new List<Guid>(); 
        }

    }
}
