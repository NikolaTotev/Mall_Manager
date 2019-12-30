using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Windows.Forms;


namespace Core
{
    public static class SerializationManager
    {
        static readonly string m_LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static readonly string m_ProgramFolder = System.IO.Path.Combine(m_LocalAppDataPath, "MallManager_DATA");
        static readonly string m_ActivityManagerSave = System.IO.Path.Combine(m_ProgramFolder, "ActivityManager.json");
        //TODO create path for room serializationFile
        static readonly string m_RoomManagerSave = System.IO.Path.Combine(m_ProgramFolder, "RoomManager.json");

        public static void SaveRoomManager(RoomManager managerToSave) //TODO Add room parameter;
        {
            //Create serialization code;
            using (StreamWriter file = File.CreateText(m_RoomManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, managerToSave);
            }
        }

        /// <summary>
        /// Gets last save of room manager. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static RoomManager GetRoomManager()
        {
            if (!File.Exists(m_RoomManagerSave))
            {
                RoomManager managerToReturn = RoomManager.GetInstance();
                ExceptionManager.OnFileNotFound(m_RoomManagerSave);
                SaveRoomManager(managerToReturn);
                return managerToReturn;
            }

            using (StreamReader file = File.OpenText(m_RoomManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (RoomManager)serializer.Deserialize(file, typeof(RoomManager));
            }
        }

        /// <summary>
        /// Saves the activity that is passed to the function.
        /// </summary>
        /// <param name="managerToSave"></param>
        public static void SaveActivityManager(ActivityManager managerToSave)
        {
            using (StreamWriter file = File.CreateText(m_ActivityManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, managerToSave);
            }
        }

        /// <summary>
        /// Gets last save of activity manager. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static ActivityManager GetActivityManager()
        {

            if (!File.Exists(m_ActivityManagerSave))
            {
                ActivityManager managerToReturn = ActivityManager.GetInstance();
                ExceptionManager.OnFileNotFound(m_ActivityManagerSave);
                SaveActivityManager(managerToReturn);
                return managerToReturn;
            }

            using (StreamReader file = File.OpenText(m_ActivityManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (ActivityManager)serializer.Deserialize(file, typeof(ActivityManager));
            }
        }
    }
}
