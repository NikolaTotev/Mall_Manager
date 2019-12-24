using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;

using System.IO;
using System.Windows.Forms;

namespace Core
{
    public static class SerializationManager
    {
        static readonly string m_LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static readonly string m_ProgramFolder = System.IO.Path.Combine(m_LocalAppDataPath, "MallManager_DATA");
        static readonly string m_ActivityManagerSave = System.IO.Path.Combine(m_ProgramFolder, "ActivityManager.json");
        //TODO create path for room serializationFile

        public static void SerializeRoomManager() //TODO Add room parameter;
        {
            //Create serialization code;
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
                ActivityManager managerToReturn = new ActivityManager(ProgramManager.GetInstance());
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
