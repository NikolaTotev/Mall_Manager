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
        public static readonly string LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string ProgramFolder = System.IO.Path.Combine(LocalAppDataPath, "MallManager_DATA");
        public static readonly string ActivityManagerSave = System.IO.Path.Combine(ProgramFolder, "ActivityManager.json");
        public static readonly string ActivityConfigSave = System.IO.Path.Combine(ProgramFolder, "ActivityConfig.json");
        public static readonly string RoomManagerSave = System.IO.Path.Combine(ProgramFolder, "RoomManager.json");

        public static void CheckForDirectory()
        {
            if (!Directory.Exists(ProgramFolder))
            {
                Directory.CreateDirectory(ProgramFolder);
            }
        }

        public static void SaveRoomManager(RoomManager managerToSave) //TODO Add room parameter;
        {
            //Create serialization code;
            using (StreamWriter file = File.CreateText(RoomManagerSave))
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
            if (!File.Exists(RoomManagerSave))
            {
                RoomManager managerToReturn = RoomManager.GetInstance();
                ExceptionManager.OnFileNotFound(RoomManagerSave);
                SaveRoomManager(managerToReturn);
                return managerToReturn;
            }

            using (StreamReader file = File.OpenText(RoomManagerSave))
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
            using (StreamWriter file = File.CreateText(ActivityManagerSave))
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

            if (!File.Exists(ActivityManagerSave))
            {
                ActivityManager managerToReturn = ActivityManager.GetInstance();
                ExceptionManager.OnFileNotFound(ActivityManagerSave);
                SaveActivityManager(managerToReturn);
                return managerToReturn;
            }

            using (StreamReader file = File.OpenText(ActivityManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                return (ActivityManager)serializer.Deserialize(file, typeof(ActivityManager));
            }
        }

        public static void SaveActivityConfigFile(ActivityConfig configToSave)
        {
            CheckForDirectory();
            using (StreamWriter file = File.CreateText(ActivityConfigSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, configToSave);
            }
        }

        //TODO Create read configFile method.
    }
}
