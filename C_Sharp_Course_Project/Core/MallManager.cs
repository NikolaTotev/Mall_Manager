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
        public string CurrentMallName { get; set; }

        public Dictionary<Guid, Mall> Malls { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        private MallManager()
        {
            m_Instance = this;
            Malls = SerializationManager.GetMalls();
            CurrentMall = null;
            CurrentMallName = "";
        }

        /// <summary>
        /// Singleton pattern. Gets the instance of the MallManager if there is one.
        /// If one doesn't exist a new one is created.
        /// </summary>
        /// <returns></returns>
        public static MallManager GetInstance()
        {
            return m_Instance ?? (m_Instance = new MallManager());
        }

        /// <summary>
        /// Adds a new mall based on the given parameters.
        /// </summary>
        /// <param name="mallToAdd"></param>
        /// <returns></returns>
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
            CurrentMallName = CurrentMall.Name;
            SerializationManager.SaveMalls(Malls);
            ProgramManager.GetInstance().CompleteInitialization();
            return true;
        }

        /// <summary>
        /// Changes a mall based on input.
        /// </summary>
        /// <param name="editedMall"></param>
        public void EditMall(Mall editedMall)
        {
            Mall mallToEdit = Malls[editedMall.Id];

            if (editedMall.HasValidData())
            {
                mallToEdit.Name = editedMall.Name;
                mallToEdit.Description = editedMall.Description;

            }

            if (mallToEdit == CurrentMall)
            {
                CurrentMall = Malls[editedMall.Id];
                CurrentMallName = CurrentMall.Name;
            }

            SerializationManager.SaveMalls(Malls);
        }

        /// <summary>
        /// Removes mall based on ID
        /// </summary>
        /// <param name="mallId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes all activities from the mall.
        /// </summary>
        public void ClearActivities()
        {
            CurrentMall.AssociatedActivities.Clear();
            SerializationManager.SaveMalls(Malls);
        }

        /// <summary>
        /// Handles changing current mall when a mall is opened from the dashboard.
        /// </summary>
        /// <param name="mallId"></param>
        /// <returns></returns>
        public bool ChangeCurrentMall(Guid mallId)
        {
            if (!Malls.ContainsKey(mallId))
            {
                return false;
            }

            CurrentMall = Malls[mallId];
            CurrentMallName = CurrentMall.Name;
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