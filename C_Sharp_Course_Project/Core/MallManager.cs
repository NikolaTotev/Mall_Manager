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
        public Mall CurrentMall { get; private set; }
        public Dictionary<string, Mall> Malls { get; set; }

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

        public bool AddMall(string mallId, string mallName, string mallDescription)
        {
            if (mallId == null || mallName == null || mallDescription == null)
            {
                ExceptionManager.OnNullParamsToFunction("Add mall");
                return false;
            }

            Mall newMall = new Mall(mallId, mallName, mallDescription);

            if (Malls.ContainsKey(newMall.Id))
            {
                return false;
            }

            Malls.Add(newMall.Id, newMall);
            //Serialization Manager save
            return true;
        }

        public bool RemoveMall(string mallId)
        {
            if (!Malls.ContainsKey(mallId))
            {
                return false;
            }

            Malls.Remove(mallId);
            //Serialization Manager save
            return true;
        }

        public void EditMall(string mallId, string mallName = null, string mallDescription = null)
        {
            Mall mallToEdit = Malls[mallId];
            if (mallName != null)
            {
                mallToEdit.Name = mallName;
            }

            if (mallDescription != null)
            {
                mallToEdit.Description = mallDescription;
            }

            //Serialization Manager Save;
        }

        public bool ChangeCurrentMall(string mallId)
        {
            if (!Malls.ContainsKey(mallId))
            {
                return false;
            }

            CurrentMall = Malls[mallId];
            return true;
        }

        public Dictionary<string, string> GetMalls()
        {
            Dictionary<string, string> mallDictionary = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var mall in Malls)
            {
                sb.Clear();
                sb.Append("Mall Name: ");
                sb.Append(mall.Value.Name);
                sb.Append("Mall Description: ");
                sb.Append(mall.Value.Description);
                mallDictionary.Add(mall.Value.Id, sb.ToString());
            }

            return mallDictionary;
        }
    }
}