using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Core
{
    public static class SerializationManager
    {
        //Base save path
        public static readonly string LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string ProgramFolder = Path.Combine(LocalAppDataPath, "MallManager_DATA");

        //Folder paths
        public static readonly string MallSaves = Path.Combine(ProgramFolder, "MallSaves");
        public static readonly string ActivitySaves = Path.Combine(ProgramFolder, "ActivitySaves");
        public static readonly string ConfigFiles = Path.Combine(ProgramFolder, "Config");
        public static readonly string RoomSaves = Path.Combine(ProgramFolder, "RoomSaves");
        public static readonly string MallSavesFile = Path.Combine(MallSaves, "Malls.json");

        //Base file names
        public static readonly string ActivitySaveFileBase = "Activities.json";
        public static readonly string ActivityConfigSaveBase = "ActivityConfig.json";
        public static readonly string RoomConfigSaveBase = "RoomConfig.json";
        public static readonly string RoomsSaveBase = "Rooms.json";

        //Utility variables
        private static readonly JsonSerializer m_Serializer = new JsonSerializer();
        private static readonly StringBuilder StringBuilder = new StringBuilder("");

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
            if (!Directory.Exists(ConfigFiles))
            {
                Directory.CreateDirectory(ConfigFiles);
            }
        }

        public static void RenameFiles(string oldName, string newName)
        {
            if (File.Exists(GenActivitySaveName(oldName)))
            {
                File.Move(GenActivitySaveName(oldName), GenActivitySaveName(newName));
            }

            if (File.Exists(GenRoomSaveName(oldName)))
            {
                File.Move(GenRoomSaveName(oldName), GenRoomSaveName(newName));
            }

            if (File.Exists(GenActivityConfigName(oldName)))
            {
                File.Move(GenActivityConfigName(oldName), GenActivityConfigName(newName));
            }

            if (File.Exists(GenRoomConfigName(oldName)))
            {
                File.Move(GenRoomConfigName(oldName), GenRoomConfigName(newName));
            }

        }

        #region Name gen functions
        public static string GenRoomSaveName(string mallName)
        {
            StringBuilder.Clear();
            StringBuilder.Append(mallName);
            StringBuilder.Append(RoomsSaveBase);
            return Path.Combine(RoomSaves, StringBuilder.ToString());
        }

        public static string GenActivitySaveName(string mallName)
        {
            StringBuilder.Clear();
            StringBuilder.Append(mallName);
            StringBuilder.Append(ActivitySaveFileBase);
            return Path.Combine(ActivitySaves, StringBuilder.ToString());
        }

        public static string GenActivityConfigName(string mallName)
        {
            StringBuilder.Clear();
            StringBuilder.Append(mallName);
            StringBuilder.Append(ActivityConfigSaveBase);
            return Path.Combine(ConfigFiles, StringBuilder.ToString());
        }

        public static string GenRoomConfigName(string mallName)
        {
            StringBuilder.Clear();
            StringBuilder.Append(mallName);
            StringBuilder.Append(RoomConfigSaveBase);
            return Path.Combine(ConfigFiles, StringBuilder.ToString());
        }

        #endregion


        public static void SaveMalls(Dictionary<Guid, Mall> mallsToSave)
        {
            using (StreamWriter file = File.CreateText(MallSavesFile))
            {
                m_Serializer.Serialize(file, mallsToSave);
            }
        }

        /// <summary>
        /// Gets last save of the room dictionary. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Guid, Mall> GetMalls()
        {

            if (!File.Exists(MallSavesFile))
            {
                Dictionary<Guid, Mall> dictionaryToReturn = new Dictionary<Guid, Mall>();
                ExceptionManager.OnFileNotFound(MallSavesFile);
                SaveMalls(dictionaryToReturn);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(MallSavesFile))
            {
                return (Dictionary<Guid, Mall>)m_Serializer.Deserialize(file, typeof(Dictionary<Guid, Mall>));
            }
        }

        #region Room read/write functions
        public static void SaveRooms(Dictionary<Guid, Room> roomsToSave, string mallName)
        {
            using (StreamWriter file = File.CreateText(GenRoomSaveName(mallName)))
            {
                m_Serializer.Serialize(file, roomsToSave);
            }
        }

        /// <summary>
        /// Gets last save of the room dictionary. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Guid, Room> GetRooms(string mallName)
        {

            if (!File.Exists(GenRoomSaveName(mallName)))
            {
                Dictionary<Guid, Room> dictionaryToReturn = new Dictionary<Guid, Room>();
                ExceptionManager.OnFileNotFound(GenRoomSaveName(mallName));
                SaveRooms(dictionaryToReturn, mallName);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(GenRoomSaveName(mallName)))
            {
                return (Dictionary<Guid, Room>)m_Serializer.Deserialize(file, typeof(Dictionary<Guid, Room>));
            }
        }

        #endregion

        #region Activity read/write functions
        public static void SaveActivities(Dictionary<Guid, Activity> activitiesToSave, string mallName)
        {

            using (StreamWriter file = File.CreateText(GenActivitySaveName(mallName)))
            {
                m_Serializer.Serialize(file, activitiesToSave);
            }
        }

        /// <summary>
        /// Gets last save of the activity dictionary. If no save is found a new one is created.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Guid, Activity> GetActivities(string mallName)
        {
            if (!File.Exists(GenActivitySaveName(mallName)))
            {
                Dictionary<Guid, Activity> dictionaryToReturn = new Dictionary<Guid, Activity>();
                ExceptionManager.OnFileNotFound(GenActivitySaveName(mallName));
                SaveActivities(dictionaryToReturn, mallName);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(GenActivitySaveName(mallName)))
            {
                return (Dictionary<Guid, Activity>)m_Serializer.Deserialize(file, typeof(Dictionary<Guid, Activity>));
            }
        }

        #endregion

        #region Config read/write functions
        public static void SaveActivityConfigFile(ActivityConfig configToSave, string mallName)
        {
            CheckForDirectories();
            using (StreamWriter file = File.CreateText(GenActivityConfigName(mallName)))
            {
                m_Serializer.Serialize(file, configToSave);
            }
        }

        public static ActivityConfig GetActivityConfig(string mallName)
        {
            if (!File.Exists(GenActivityConfigName(mallName)))
            {
                ActivityConfig activityConfigToReturn = new ActivityConfig();
                activityConfigToReturn.Categories.Add("Cleaning");
                activityConfigToReturn.Categories.Add("Maintenance");
                activityConfigToReturn.Categories.Add("Other");
                ExceptionManager.OnFileNotFound(GenActivityConfigName(mallName));
                SaveActivityConfigFile(activityConfigToReturn, mallName);
                return activityConfigToReturn;
            }

            using (StreamReader file = File.OpenText(GenActivityConfigName(mallName)))
            {
                return (ActivityConfig)m_Serializer.Deserialize(file, typeof(ActivityConfig));
            }
        }

        public static void SaveRoomConfigFile(RoomConfig configToSave, string mallName)
        {
            CheckForDirectories();
            using (StreamWriter file = File.CreateText(GenRoomConfigName(mallName)))
            {
                m_Serializer.Serialize(file, configToSave);
            }
        }

        public static RoomConfig GetRoomConfig(string mallName)
        {
            if (!File.Exists(GenRoomConfigName(mallName)))
            {
                RoomConfig roomConfigToReturn = new RoomConfig();
                roomConfigToReturn.RoomTypes.Add("Store");
                roomConfigToReturn.RoomTypes.Add("Restaurant");
                roomConfigToReturn.RoomTypes.Add("Bathroom");
                roomConfigToReturn.RoomTypes.Add("Other");
                ExceptionManager.OnFileNotFound(GenRoomConfigName(mallName));
                SaveRoomConfigFile(roomConfigToReturn, mallName);
                return roomConfigToReturn;
            }

            using (StreamReader file = File.OpenText(GenRoomConfigName(mallName)))
            {
                return (RoomConfig)m_Serializer.Deserialize(file, typeof(RoomConfig));
            }
        }
        #endregion

    }
}
