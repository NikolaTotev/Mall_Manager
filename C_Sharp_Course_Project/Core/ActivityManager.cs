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
        Finished,
        Failed,
        Undefined
    }

    public class ActivityManager
    {
        private ProgramManager m_CurrentManager;
        private static ActivityManager m_Instance;
        private ActivityConfig m_Config;
        public Dictionary<Guid, Activity> Activities { get; set; }


        private ActivityManager()
        {
            m_Instance = this;
            m_CurrentManager = ProgramManager.GetInstance();
            m_Config = SerializationManager.GetActivityConfig(MallManager.GetInstance().CurrentMall.Name);
            Activities = SerializationManager.GetActivities(MallManager.GetInstance().CurrentMall.Name);
        }

        public static ActivityManager GetInstance()
        {
            return m_Instance ?? (m_Instance = new ActivityManager());
        }

        public List<string> GetCategories()
        {
            return m_Config.Categories;
        }

        public bool AddNewCategory(string category,string mallName)
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
            SerializationManager.SaveActivityConfigFile(m_Config, mallName);
            return true;
        }

        public bool AddNewTemplate(string category, string description,string mallName)
        {
            if (category == null || description == null)
            {
                ExceptionManager.OnNullParamsToFunction("Add New Activity Template");
                return false;
            }

            Activity newActivityTemplate = new Activity(Guid.Empty , Guid.Empty, category, true, description);
            m_Config.Templates.Add(newActivityTemplate);
            SerializationManager.SaveActivityConfigFile(m_Config, mallName);
            return true;
        }

        public bool AddActivity(Activity activityToAdd,string mallName)
        {
            //if (newId == null || category == null || description == null || corespRoom == null)
            //{
            //    ExceptionManager.OnNullParamsToFunction("Add activity");
            //    return false;
            //}

            //if (startDate > endDate)
            //{
            //    ExceptionManager.OnInvalidDate(); //Maybe other exception Invalid Date
            //    return false;
            //}

            RoomManager roomManager = RoomManager.GetInstance();
            if (activityToAdd.CorrespondingRoom == MallManager.GetInstance().CurrentMall.Id && !Activities.ContainsKey(activityToAdd.Id))
            {
                MallManager.GetInstance().CurrentMall.AssociatedActivities.Add(activityToAdd.Id);
                SerializationManager.SaveMalls(MallManager.GetInstance().Malls);
            }
            else if(Activities.ContainsKey(activityToAdd.Id) || !roomManager.AddActivity(activityToAdd.CorrespondingRoom, activityToAdd.Id,mallName))
            {
                return false;
            }

            Activities.Add(activityToAdd.Id, activityToAdd);
            SerializationManager.SaveActivities(Activities,mallName);
            return true;
        }

        public void EditActivity(string mallName, Guid activityId, string category = null, string description = null, ActivityStatus status = ActivityStatus.Undefined, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime))
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
            if (DateTime.Compare(startDate, default(DateTime)) != 0 && DateTime.Compare(endDate, default(DateTime)) != 0 && DateTime.Compare(startDate, endDate) < 0)
            {
                activityToEdit.StartTime = startDate;
                activityToEdit.EndTime = endDate;
            }

            SerializationManager.SaveActivities(Activities, mallName);
        }

        public bool DeleteActivity(Guid activityId,string mallName)
        {
            if (!Activities.ContainsKey(activityId))
            {
                return false;
            }

            RoomManager roomManager = RoomManager.GetInstance();
            if(!roomManager.DeleteActivity(Activities[activityId].CorrespondingRoom, activityId, mallName))
            {
                return false;
            }

            Activities.Remove(activityId);
            SerializationManager.SaveActivities(Activities, mallName);
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

    }
}
