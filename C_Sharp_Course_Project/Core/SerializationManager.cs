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
        static string m_LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string m_ProgramFolder = System.IO.Path.Combine(m_LocalAppDataPath, "MallManager_DATA");
        static string m_ActivityManagerSave = System.IO.Path.Combine(m_ProgramFolder, "ActivityManager.json");



        public static void SerializeRoom() //TODO Add room parameter;
        {
            //Create serialization code;
        }

        public static void SaveActivityManager(ActivityManager managerToSave)
        {
            using (StreamWriter file = File.CreateText(m_ActivityManagerSave))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, managerToSave);
            }
        }

        public static ActivityManager GetActivityManager()
        {

            if (!File.Exists(m_ActivityManagerSave))
            {
                ActivityManager managerToReturn = new ActivityManager();
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
