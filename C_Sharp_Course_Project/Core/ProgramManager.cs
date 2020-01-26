using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core
{

    public class ProgramManager
    {
        private static ProgramManager m_Instance;
        private static MallManager m_MallManagerInstance;
        private static ActivityManager m_ActivityManagerInstance;
        private static RoomManager m_RoomManagerInstance;

        private ProgramManager()
        {
            m_Instance = this;
            SerializationManager.CheckForDirectories();
            m_MallManagerInstance = MallManager.GetInstance();
            //TODO ensure there is a mall and that it has a name. 
            m_ActivityManagerInstance = ActivityManager.GetInstance();
            m_RoomManagerInstance = RoomManager.GetInstance();
        }

        public static ProgramManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ProgramManager();
            }
            return m_Instance;
        }
    }
}