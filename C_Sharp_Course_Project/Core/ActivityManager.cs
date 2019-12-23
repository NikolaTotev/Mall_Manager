using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    enum ActivityStatus
    {
        Scheduled,
        InProgress,
        Finished
    }

    class ActivityManager
    {
        private ProgramManager m_CurrentManager;

        ActivityManager(ProgramManager currentManager)
        {
            m_CurrentManager = currentManager;
        }
    }
}
