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
        private  ProgramManager m_CurrentManager;

        public List<string> Categories { get; set; }
        public List<string> Templates { get; set; }


        public ActivityManager(ProgramManager currentManager)
        {
            m_CurrentManager = currentManager;
        }

        public void Save()
        {
            SerializationManager.SaveActivityManager(this);
        }
    }
}
