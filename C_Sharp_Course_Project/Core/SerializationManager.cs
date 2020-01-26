﻿using System;
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
        //Base save path
        public static readonly string LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static readonly string ProgramFolder = System.IO.Path.Combine(LocalAppDataPath, "MallManager_DATA");

        //Folder paths
        public static readonly string MallSaves = System.IO.Path.Combine(ProgramFolder, "MallSaves");
        public static readonly string ActivitySaves = System.IO.Path.Combine(ProgramFolder, "ActivitySaves");
        public static readonly string ConfigFiles = System.IO.Path.Combine(ProgramFolder, "Config");
        public static readonly string RoomSaves = System.IO.Path.Combine(ProgramFolder, "RoomSaves");

        //Base file names
        public static readonly string ActivitySaveFileBase = "Activities.json";
        public static readonly string ActivityConfigSaveBase = "ActivityConfig.json";
        public static readonly string RoomsSaveBase = "Rooms.json";

        //Utility variables
        private static JsonSerializer m_Serializer = new JsonSerializer();
        private static StringBuilder m_StringBuilder = new StringBuilder("");

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

        #region Name gen functions
        public static string GenRoomSaveName(string mallName)
        {
            m_StringBuilder.Clear();
            m_StringBuilder.Append(mallName);
            m_StringBuilder.Append(RoomsSaveBase);
            return System.IO.Path.Combine(RoomSaves, m_StringBuilder.ToString());
        }

        public static string GenActivitySaveName(string mallName)
        {
            m_StringBuilder.Clear();
            m_StringBuilder.Append(mallName);
            m_StringBuilder.Append(ActivitySaveFileBase);
            return System.IO.Path.Combine(ActivitySaves, m_StringBuilder.ToString());
        }

        public static string GenActivityConfigName(string mallName)
        {
            m_StringBuilder.Clear();
            m_StringBuilder.Append(mallName);
            m_StringBuilder.Append(ActivityConfigSaveBase);
            return System.IO.Path.Combine(ConfigFiles, m_StringBuilder.ToString());
        }

        #endregion

        #region Room read/write functions
        public static void SaveRooms(Dictionary<string, Room> roomsToSave, string mallName)
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
        public static Dictionary<string, Room> GetRooms(string mallName)
        {

            if (!File.Exists(GenRoomSaveName(mallName)))
            {
                Dictionary<string, Room> dictionaryToReturn = new Dictionary<string, Room>();
                ExceptionManager.OnFileNotFound(GenRoomSaveName(mallName));
                SaveRooms(dictionaryToReturn, mallName);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(GenRoomSaveName(mallName)))
            {
                return (Dictionary<string, Room>)m_Serializer.Deserialize(file, typeof(Dictionary<string, Room>));
            }
        }

        #endregion

        #region Activity read/write functions
        public static void SaveActivities(Dictionary<string, Activity> activitiesToSave, string mallName)
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
        public static Dictionary<string, Activity> GetActivities(string mallName)
        {
            if (!File.Exists(GenActivitySaveName(mallName)))
            {
                Dictionary<string, Activity> dictionaryToReturn = new Dictionary<string, Activity>();
                ExceptionManager.OnFileNotFound(GenActivitySaveName(mallName));
                SaveActivities(dictionaryToReturn, mallName);
                return dictionaryToReturn;
            }

            using (StreamReader file = File.OpenText(GenActivitySaveName(mallName)))
            {
                return (Dictionary<string, Activity>)m_Serializer.Deserialize(file, typeof(Dictionary<string, Activity>));
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
                ExceptionManager.OnFileNotFound(GenActivityConfigName(mallName));
                SaveActivityConfigFile(activityConfigToReturn, mallName);
                return activityConfigToReturn;
            }

            using (StreamReader file = File.OpenText(GenActivityConfigName(mallName)))
            {
                return (ActivityConfig)m_Serializer.Deserialize(file, typeof(ActivityConfig));
            }
        }


        #endregion

    }
}
