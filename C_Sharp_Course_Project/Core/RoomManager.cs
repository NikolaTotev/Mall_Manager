using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RoomManager
    {
        ///TODO Implement singleton pattern (see program manager for example)

        private static RoomManager m_Instance;
        public List<string> RoomTypes { get; set; }
        
        private RoomManager()
        {
            m_Instance = this;
        }

        public static RoomManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new RoomManager();
            }
            return m_Instance;
        }
    }
}
