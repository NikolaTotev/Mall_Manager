using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Windows.Forms;

namespace Core
{
    public class MallManager
    {
        private static MallManager m_Instance;

        private MallManager()
        {
            m_Instance = this;
        }

        public static MallManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new MallManager();
            }
            return m_Instance;
        }
        public Mall CurrentMall { get; set; }
    }
}