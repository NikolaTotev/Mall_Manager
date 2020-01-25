using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RoomManager
    {
        ///TODO Implement singleton pattern (see program manager for example)

        private static RoomManager m_Instance;
        public List<string> RoomTypes { get; set; }

        public Dictionary<Guid, Room> Rooms { get; set; }

        private RoomManager()
        {
            m_Instance = this;
            Rooms = new Dictionary<Guid, Room>();
        }

        public static RoomManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new RoomManager();
            }
            return m_Instance;
        }

        public bool CreateRoom(string name, string description, string roomType, int floorNumber, int roomNumber)
        {
            if (name == null || description == null || roomType == null)
            {
                ExceptionManager.OnNullParamsToFunction("Create room");
                return false;
            }

            Room newRoom = new Room(name, description, roomType, floorNumber, roomNumber);
            Guid newRoomGuid = newRoom.ID;

            //If statement just in case.
            if (Rooms.ContainsKey(newRoomGuid))
            {
                return false;
            }

            Rooms.Add(newRoomGuid, newRoom);
            return true;
        }

        public Dictionary<string, string> GetRooms()
        {
            Dictionary<string, string> roomDictionary = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var room in Rooms)
            {
                sb.Clear();
                sb.Append(room.Value.Name);
                sb.Append("Floor Number: ");
                sb.Append(room.Value.Floor.ToString());
                sb.Append("Room Number: ");
                sb.Append(room.Value.RoomNumber.ToString());
                roomDictionary.Add(room.Value.ID.ToString(), sb.ToString());
            }

            return roomDictionary;
        }
    }
}
