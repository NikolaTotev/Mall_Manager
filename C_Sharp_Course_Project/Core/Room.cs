using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public readonly Guid ID;
        public List<Guid> Activities { get; set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastEditDate { get; private set; }

        public Room(string name, string description, string type, int roomNumber, int floorNumber)
        {
            Name = name;
            Description = description;
            Type = type;
            Floor = floorNumber;
            RoomNumber = roomNumber;
            ID = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
            LastEditDate = DateTime.UtcNow;
        }

        public void EditRoom(string name = null, string description = null, string type = null)
        {
            LastEditDate = DateTime.UtcNow;
            if (name != null)
            {
                Name = name;
            }
            if (description != null)
            {
                Description = description;
            }
            if (type != null)
            {
                Type = type;
            }
        }
        //TODO constructors, and methods for editing rooms and deleting, and list of activities for this room
    }
}
