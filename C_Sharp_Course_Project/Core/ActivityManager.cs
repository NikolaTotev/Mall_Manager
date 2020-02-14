using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Core
{
    public enum ActivityStatus
    {
        Scheduled,
        InProgress,
        Finished,
        Failed,
        Undefined
    }

    public class ActivityManager
    {
        private static ActivityManager m_Instance;
        private ActivityConfig m_Config;
        public Dictionary<Guid, Activity> Activities { get; set; }

        public event EventHandler ActivitiesChanged;

        private ActivityManager()
        {
            m_Instance = this;
            ProgramManager.GetInstance();
            m_Config = SerializationManager.GetActivityConfig(MallManager.GetInstance().CurrentMall.Name);
            Activities = SerializationManager.GetActivities(MallManager.GetInstance().CurrentMall.Name);
        }

        public static ActivityManager GetInstance()
        {
            return m_Instance ?? (m_Instance = new ActivityManager());
        }

        public void ReloadManager()
        {
            m_Config = SerializationManager.GetActivityConfig(MallManager.GetInstance().CurrentMall.Name);
            Activities = SerializationManager.GetActivities(MallManager.GetInstance().CurrentMall.Name);
        }

   
        public bool AddActivity(Activity activityToAdd, string mallName)
        {
            RoomManager roomManager = RoomManager.GetInstance();
            if (activityToAdd.CorrespondingRoom == MallManager.GetInstance().CurrentMall.Id && !Activities.ContainsKey(activityToAdd.Id))
            {
                MallManager.GetInstance().CurrentMall.AssociatedActivities.Add(activityToAdd.Id);
                SerializationManager.SaveMalls(MallManager.GetInstance().Malls);
            }
            else if (Activities.ContainsKey(activityToAdd.Id) || !roomManager.AddActivity(activityToAdd.CorrespondingRoom, activityToAdd.Id, mallName))
            {
                return false;
            }

            Activities.Add(activityToAdd.Id, activityToAdd);
            SerializationManager.SaveActivities(Activities, mallName);
            OnActivitiesChanged();
            return true;
        }

        public void EditActivity(Guid activityId, string category = null, string description = null,
            ActivityStatus status = ActivityStatus.Undefined, DateTime startDate = default,
            DateTime endDate = default)
        {
            Activity activityToEdit = Activities[activityId];
            if (category != null)
            {
                activityToEdit.Category = category;
            }
            if (description != null)
            {
                activityToEdit.Description = description;
            }
            if (status != ActivityStatus.Undefined)
            {
                activityToEdit.CurActivityStatus = status;
            }
            if (DateTime.Compare(startDate, default) != 0 && DateTime.Compare(endDate, default) != 0 && DateTime.Compare(startDate, endDate) < 0)
            {
                activityToEdit.StartTime = startDate;
                activityToEdit.EndTime = endDate;
            }

            SerializationManager.SaveActivities(Activities, MallManager.GetInstance().CurrentMallName);
            OnActivitiesChanged();
        }

        public void EditActivitiesStatus(List<Guid> activityId, ActivityStatus status)
        {
            foreach (var activity in activityId)
            {
                EditActivityStatus(activity, status);
            }
            SerializationManager.SaveActivities(Activities, MallManager.GetInstance().CurrentMallName);
            OnActivitiesChanged();
        }

        private void EditActivityStatus(Guid activityId, ActivityStatus status)
        {
            Activity activityToEdit = Activities[activityId];
            if (status != ActivityStatus.Undefined)
            {
                activityToEdit.CurActivityStatus = status;
            }
        }


        /// <summary>
        /// Deletes activity from given and mall ID
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public bool DeleteActivity(Guid activityId)
        {
            if (!Activities.ContainsKey(activityId))
            {
                return false;
            }

            RoomManager roomManager = RoomManager.GetInstance();
            if (!roomManager.DeleteActivity(Activities[activityId].CorrespondingRoom, activityId))
            {
                if (Activities[activityId].CorrespondingRoom == MallManager.GetInstance().CurrentMall.Id && MallManager.GetInstance().CurrentMall.AssociatedActivities.Contains(activityId))
                {
                    MallManager.GetInstance().CurrentMall.AssociatedActivities.Remove(activityId);
                    SerializationManager.SaveMalls(MallManager.GetInstance().Malls);
                }
                else
                {
                    return false;
                }
            }

            Activities.Remove(activityId);
            SerializationManager.SaveActivities(Activities, MallManager.GetInstance().CurrentMallName);
            OnActivitiesChanged();
            return true;
        }

        public void ClearActivities()
        {
            Activities.Clear();
            MallManager.GetInstance().ClearActivities();
            RoomManager.GetInstance().ClearRoomActivities();
            SerializationManager.SaveActivities(Activities, MallManager.GetInstance().CurrentMall.Name);
        }

        public void ClearRoomActivities(Room room)
        {
            while (room.Activities.Count > 0)
            {
                DeleteActivity(room.Activities[0]);
            }
        }


        public bool AddNewCategory(string category)
        {
            if (category == null)
            {
                ExceptionManager.OnNullParamsToFunction(nameof(AddNewCategory));
                return false;
            }

            if (m_Config.Categories.Contains(category))
            {
                ExceptionManager.OnCreateDuplicateCategory(category);
                return false;
            }
            m_Config.Categories.Add(category);
            SerializationManager.SaveActivityConfigFile(m_Config, MallManager.GetInstance().CurrentMallName);
            m_Config = SerializationManager.GetActivityConfig(MallManager.GetInstance().CurrentMall.Name);
            return true;
        }

        public bool RemoveCategory(string category)
        {
            if (category == null)
            {
                ExceptionManager.OnNullParamsToFunction(nameof(AddNewCategory));
                return false;
            }

            if (!m_Config.Categories.Contains(category))
            {
                ExceptionManager.OnCreateDuplicateCategory(category);
                return false;
            }
            m_Config.Categories.Remove(category);
            SerializationManager.SaveActivityConfigFile(m_Config, MallManager.GetInstance().CurrentMallName);
            m_Config = SerializationManager.GetActivityConfig(MallManager.GetInstance().CurrentMall.Name);
            return true;
        }


        public List<string> GetCategories()
        {
            return m_Config.Categories;
        }


        public bool AddNewTemplate(string category, string description, string mallName)
        {
            if (category == null || description == null)
            {
                ExceptionManager.OnNullParamsToFunction("Add New Activity Template");
                return false;
            }

            Activity newActivityTemplate = new Activity(Guid.Empty, Guid.Empty, category, true, description);
            m_Config.Templates.Add(newActivityTemplate);
            SerializationManager.SaveActivityConfigFile(m_Config, mallName);
            return true;
        }

        public Dictionary<Guid, string> GetActivities()
        {
            Dictionary<Guid, string> activityDictionary = new Dictionary<Guid, string>();
            StringBuilder sb = new StringBuilder("");
            foreach (var activity in Activities)
            {
                sb.Clear();
                sb.Append(activity.Value.Category);
                sb.Append("Corresponding room: ");
                sb.Append(activity.Value.CorrespondingRoom.ToString());
                sb.Append("Current status: ");
                sb.Append(activity.Value.CurActivityStatus.ToString());
                activityDictionary.Add(activity.Value.Id, sb.ToString());
            }
            return activityDictionary;
        }

        protected virtual void OnActivitiesChanged()
        {
            ActivitiesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
