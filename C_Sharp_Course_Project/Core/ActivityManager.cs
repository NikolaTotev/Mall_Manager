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
            //TODO create new category
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

        public List<string> GetCreateActivityData()
        {
            throw new NotImplementedException();
            //TODO get info from the UI
        }

        public void AddActivity()
        {
            List<string> data = GetCreateActivityData();
            //Create Activity and assign it to a room
        } 

        public void Save()
        {
            SerializationManager.SaveActivityManager(this);
        }
    }
}
