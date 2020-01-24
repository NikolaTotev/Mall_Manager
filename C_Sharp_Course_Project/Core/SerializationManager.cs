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
        public static readonly string MallSaves = System.IO.Path.Combine(ProgramFolder, "MallSaves");
        public static readonly string ActivitySaves = System.IO.Path.Combine(ProgramFolder, "ActivitySaves");
        public static readonly string RoomSaves = System.IO.Path.Combine(ProgramFolder, "RoomSaves");

        public static readonly string ActivityManagerSave = System.IO.Path.Combine(ProgramFolder, "ActivityManager.json");
        public static readonly string ActivityConfigSave = System.IO.Path.Combine(ProgramFolder, "ActivityConfig.json");
        public static readonly string RoomManagerSave = System.IO.Path.Combine(ProgramFolder, "RoomManager.json");
        private static  JsonSerializer serializer = new JsonSerializer();

        public static void CheckForDirectories()
        {
            if (!Directory.Exists(ProgramFolder))
            {
                Directory.CreateDirectory(ProgramFolder);
            }

            if (!Directory.Exists(MallSaves))
            {
                Directory.CreateDirectory(MallSaves);
            }

            if (!Directory.Exists(ActivitySaves))
            {
                Directory.CreateDirectory(ActivitySaves);
            }

            if (!Directory.Exists(RoomSaves))
            {
                Directory.CreateDirectory(RoomSaves);
            }
        }

        

        public static void SaveRoomManager(RoomManager managerToSave)
        {
            //Create serialization code;
            using (StreamWriter file = File.CreateText(RoomManagerSave))
            {
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
                return (ActivityManager)serializer.Deserialize(file, typeof(ActivityManager));
            }
        }

        public static void SaveActivityConfigFile(ActivityConfig configToSave)
        {
            CheckForDirectories();
            using (StreamWriter file = File.CreateText(ActivityConfigSave))
            {
                serializer.Serialize(file, configToSave);
            }
        }

        public static ActivityConfig GetActivityConfig()
        {
            throw new NotImplementedException();
        }
        //TODO Create read configFile method.
    }
}
