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
        public Dictionary<Guid, Mall> Malls { get; set; }

        private MallManager()
        {
            m_Instance = this;
            Malls = SerializationManager.GetMalls();
            CurrentMall = null;
        }

        public static MallManager GetInstance()
        {
            return m_Instance ?? (m_Instance = new MallManager());
        }

        public bool AddMall(Mall mallToAdd)
        {
            if (!mallToAdd.HasValidData())
            {
                ExceptionManager.OnNullParamsToFunction("Add mall");
                return false;
            }
            
            if (Malls.ContainsKey(mallToAdd.Id))
            {
                return false;
            }

            Malls.Add(mallToAdd.Id, mallToAdd);
            CurrentMall = Malls[mallToAdd.Id];
            SerializationManager.SaveMalls(Malls);
            ProgramManager.GetInstance().CompleteInitialization();
            return true;
        }

        public bool RemoveMall(Guid mallId)
        {
            if (!Malls.ContainsKey(mallId))
            {
                return false;
            }

            Malls.Remove(mallId);
            //Serialization Manager save
            return true;
        }

        public void EditMall(Guid mallId, string mallName = null, string mallDescription = null)
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

        public bool ChangeCurrentMall(Guid mallId)
        {
            if (!Malls.ContainsKey(mallId))
            {
                return false;
            }

            CurrentMall = Malls[mallId];
            ProgramManager.GetInstance().CompleteInitialization();
            RoomManager.GetInstance().ReloadManager();
            ActivityManager.GetInstance().ReloadManager();
            return true;
        }

        public Dictionary<Guid, string> GetMalls()
        {
            Dictionary<Guid, string> mallDictionary = new Dictionary<Guid, string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var mall in Malls)
            {
                sb.Clear();
                sb.Append(mall.Value.Name);
                sb.Append(" ");
                sb.Append(mall.Value.Description);
                mallDictionary.Add(mall.Value.Id, sb.ToString());
            }

            return mallDictionary;
        }
    }
}