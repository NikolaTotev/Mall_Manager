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

        private ProgramManager()
        {
            m_Instance = this;
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