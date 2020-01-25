using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum ActivityStatus
    {
        Scheduled,
        InProgress,
        Finished,
        Undefined
    }

    public class ActivityManager
    {
        private ProgramManager m_CurrentManager;
        private static ActivityManager m_Instance;
        //Setup  class
        private ActivityConfig m_Config;
        public Dictionary<Guid, Activity> Activities { get; set; }
        //public List<string> Categories { get; set; }
        //public List<string> Templates { get; set; }


        private ActivityManager()
        {
            m_Instance = this;
            m_CurrentManager = ProgramManager.GetInstance();
            m_Config = SerializationManager.GetActivityConfig();
            //Deserialize setup class file.
        }

        public static ActivityManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ActivityManager();
            }

            return m_Instance;
        }
     
        public bool AddNewCategory(string category)
        {
            if (category == null)
            {
                ExceptionManager.OnNullParamsToFunction(nameof(AddNewCategory));
                return false;
            }

            if(m_Config.Categories.Contains(category))
            {
                ExceptionManager.OnCreateDuplicateCategory(category);
                return false;
            }
            m_Config.Categories.Add(category);
            SerializationManager.SaveActivityConfigFile(m_Config);
            return true;
        }

        public void AddNewTemplate()
        {
            //TODO crete new template
        }

        public void AddActivity()
        {
            //Create Activity and assign it to a room
        }

        public Dictionary<string, string> GetActivities()
        {
            Dictionary<string, string> activityDictionary = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var activity in Activities)
            {
                sb.Clear();
                sb.Append(activity.Value.Category);
                sb.Append("Corresponding room: ");
                sb.Append(activity.Value.CorrespondingRoom.ToString());
                sb.Append("Current status: ");
                sb.Append(activity.Value.CurActivityStatus.ToString());
                activityDictionary.Add(activity.Key.ToString(), sb.ToString());
            }
            return activityDictionary;
        }

        public void Save()
        {
            SerializationManager.SaveActivityManager(this);
        }
    }
}
