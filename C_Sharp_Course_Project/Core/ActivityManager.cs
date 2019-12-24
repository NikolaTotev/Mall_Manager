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
        Finished
    }

    public class ActivityManager
    {
        private ProgramManager m_CurrentManager;
        private static ActivityManager instance;
        public List<string> Categories { get; set; }
        public List<string> Templates { get; set; }


        public ActivityManager(ProgramManager currentManager)
        {
            instance = this;
            m_CurrentManager = currentManager;
        }

        public static ActivityManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ActivityManager(ProgramManager.GetInstance());
            }

            return instance;
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
