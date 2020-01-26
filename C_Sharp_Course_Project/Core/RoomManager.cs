﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RoomManager
    {
        private static RoomManager m_Instance;
        public List<string> RoomTypes { get; set; }
        public Dictionary<string, Room> Rooms { get; set; }

        private RoomManager()
        {
            m_Instance = this;
            Rooms = SerializationManager.GetRooms(MallManager.GetInstance().CurrentMall.Name);
        }

        public static RoomManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new RoomManager();
            }
            return m_Instance;
        }

        /// <summary>
        /// Creates a room only if all the input parameters are valid.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="roomType"></param>
        /// <param name="floorNumber"></param>
        /// <param name="roomNumber"></param>
        /// <returns>Returns true if the operation completed successfully and false if it failed.</returns>
        public bool CreateRoom(string name, string description, string roomType, int floorNumber, int roomNumber, string newRoomGuid, string mallName)
        {
            if (name == null || description == null || roomType == null)
            {
                ExceptionManager.OnNullParamsToFunction("Create room");
                return false;
            }

            Room newRoom = new Room(name, description, roomType, roomNumber, floorNumber, newRoomGuid);

            //If statement just in case.
            if (Rooms.ContainsKey(newRoomGuid.ToString()))
            {
                return false;
            }

            Rooms.Add(newRoomGuid.ToString(), newRoom);
            SerializationManager.SaveRooms(Rooms, mallName);
            return true;
        }

        /// <summary>
        /// Changes values of a given room only if they have a valid value.
        /// If they don't they are skipped.
        /// </summary>
        /// <param name="roomToEdit"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="roomType"></param>
        /// <param name="floorNumber"></param>
        /// <param name="roomNumber"></param>
        public void EditRoom(string mallName, string roomToEdit, string name, string description, string roomType, int floorNumber = Int32.MaxValue, int roomNumber = Int32.MaxValue)
        {
            Room currentRoom = Rooms[roomToEdit];

            if (name != null)
            {
                currentRoom.Name = name;
            }

            if (description != null)
            {
                currentRoom.Description = description;
            }

            if (floorNumber != Int32.MaxValue)
            {
                currentRoom.Floor = floorNumber;
            }

            if (roomNumber != Int32.MaxValue)
            {
                currentRoom.RoomNumber = roomNumber;
            }

            if (roomType != null)
            {
                currentRoom.Type = roomType;
            }

            SerializationManager.SaveRooms(Rooms, mallName);
        }

        /// <summary>
        /// Deletes a room from the Rooms and then saves the updated dictionary.
        /// </summary>
        /// <param name="roomToRemove"></param>
        /// <returns>Returns true if the operation completed successfully and false if it failed.</returns>
        public bool DeleteRoom(string roomToRemove, string mallName)
        {
            if (!Rooms.ContainsKey(roomToRemove))
            {
                return false;
            }

            Rooms.Remove(roomToRemove);
            SerializationManager.SaveRooms(Rooms, mallName);
            return true;
        }

        /// <summary>
        /// AddActivity method, adds activity from the list of activities of the specified room.
        /// Called from ActivityManager when an activity is created.
        /// </summary>
        /// <param name="roomToAddTo"></param>
        /// <param name="activityToAdd"></param>
        /// <returns>Returns true if the operation completed successfully and false if it failed.</returns>
        public bool AddActivity(string roomToAddTo, string activityToAdd, string mallName)
        {
            if (!Rooms.ContainsKey(roomToAddTo))
            {
                return false;
            }

            Room roomToEdit = Rooms[roomToAddTo];
            roomToEdit.Activities.Add(activityToAdd);
            SerializationManager.SaveRooms(Rooms, mallName);
            return true;
        }

        /// <summary>
        /// DeleteActivity method, deletes activity from the list of activities of the specified room.
        /// Called by ActivityManager when an activity is deleted.
        /// </summary>
        /// <param name="roomToRemoveFrom"></param>
        /// <param name="activityToRemove"></param>
        /// <returns>Returns true if the operation completed successfully and false if it failed.</returns>
        public bool DeleteActivity(string roomToRemoveFrom, string activityToRemove, string mallName)
        {
            if (!Rooms.ContainsKey(roomToRemoveFrom))
            {
                return false;
            }

            Room roomToEdit = Rooms[roomToRemoveFrom];

            if (!roomToEdit.Activities.Contains(activityToRemove))
            {
                return false;
            }

            roomToEdit.Activities.Remove(activityToRemove);
            SerializationManager.SaveRooms(Rooms, mallName);
            return true;
        }

        /// <summary>
        /// Returns a dictionary containing basic information about every room in the rooms dictionary.
        /// </summary>
        /// <returns>Dictionary[string,string] where they key is the room id and the second the name, floor number & room number</returns>
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
                roomDictionary.Add(room.Value.Id, sb.ToString());
            }

            return roomDictionary;
        }
    }
}
