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
        public List<string> Categories { get; set; }
        public List<string> Templates { get; set; }


        private ActivityManager()
        {
            m_Instance = this;
            m_CurrentManager = ProgramManager.GetInstance();
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
     
        public void AddNewCategory()
        {
            //TODO create new category
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
