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

        public static readonly string ActivitySaveFile = System.IO.Path.Combine(ProgramFolder, "ActivityManager.json");
        public static readonly string ActivityConfigSave = System.IO.Path.Combine(ProgramFolder, "ActivityConfig.json");
        public static readonly string RoomSaveFile = System.IO.Path.Combine(RoomSaves, "Rooms.json");
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

        

        public static void SaveRooms(Dictionary<string, Room> roomsToSave)
        {
            //Create serialization code;
            using (StreamWriter file = File.CreateText(RoomSaveFile))
            {
                serializer.Serialize(file, roomsToSave);
            }
        }

        /// <summary>
        /// Gets last save of the room dictionary. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Room> GetRooms()
        {
            if (!File.Exists(RoomSaveFile))
            {
                Dictionary<string, Room> dictionaryToReturn = new Dictionary<string, Room>();
                ExceptionManager.OnFileNotFound(RoomSaveFile);
                SaveRooms(dictionaryToReturn);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(RoomSaveFile))
            {
                return (Dictionary<string, Room>)serializer.Deserialize(file, typeof(Dictionary<string, Room>));
            }
        }

        public static void SaveActivities(Dictionary<string, Activity> activitiesToSave)
        {
            using (StreamWriter file = File.CreateText(ActivitySaveFile))
            {
                serializer.Serialize(file, activitiesToSave);
            }
        }

        /// <summary>
        /// Gets last save of the activity dictionary. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Activity> GetActivities()
        {
            if (!File.Exists(ActivitySaveFile))
            {
                Dictionary<string, Activity> dictionaryToReturn = new Dictionary<string, Activity>();
                ExceptionManager.OnFileNotFound(ActivitySaveFile);
                SaveActivities(dictionaryToReturn);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(ActivitySaveFile))
            {
                return (Dictionary<string, Activity>)serializer.Deserialize(file, typeof(Dictionary<string, Activity>));
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
            if (!File.Exists(ActivityConfigSave))
            {
                ActivityConfig activityConfigToReturn = new ActivityConfig();
                ExceptionManager.OnFileNotFound(ActivityConfigSave);
                SaveActivityConfigFile(activityConfigToReturn);
                return activityConfigToReturn;
            }

            using (StreamReader file = File.OpenText(ActivityConfigSave))
            {
                return (ActivityConfig)serializer.Deserialize(file, typeof(ActivityConfig));
            }
        }
        //TODO Create read configFile method.
    }
}
