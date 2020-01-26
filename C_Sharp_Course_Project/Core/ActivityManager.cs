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
        Undefined
    }

    public class ActivityManager
    {
        private ProgramManager m_CurrentManager;
        private static ActivityManager m_Instance;
        private ActivityConfig m_Config;
        public Dictionary<string, Activity> Activities { get; set; }


        private ActivityManager()
        {
            m_Instance = this;
            m_CurrentManager = ProgramManager.GetInstance();
            m_Config = SerializationManager.GetActivityConfig();
            Activities = SerializationManager.GetActivities();
        }

        public static ActivityManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new ActivityManager();
            }

            return m_Instance;
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
            SerializationManager.SaveActivityConfigFile(m_Config);
            return true;
        }

        public bool AddNewTemplate(string category, string description)
        {
            if (category == null || description == null)
            {
                ExceptionManager.OnNullParamsToFunction("Add New Activity Template");
                return false;
            }

            Activity newActivityTemplate = new Activity("template", category, true, description);
            m_Config.Templates.Add(newActivityTemplate);
            SerializationManager.SaveActivityConfigFile(m_Config);
            return true;
        }

        public bool AddActivity(string newId, string category, string description, string corespRoom, ActivityStatus status, DateTime startDate, DateTime endDate)
        {
            if (newId == null || category == null || description == null || corespRoom == null)
            {
                ExceptionManager.OnNullParamsToFunction("Add activity");
                return false;
            }

            int dateTimeComparison = DateTime.Compare(startDate, endDate); //validate date
            if (dateTimeComparison > 0)
            {
                ExceptionManager.OnInvalidDate(); //Maybe other exception Invalid Date
                return false;
            }

            Activity newActivity = new Activity(newId, category, false, description, corespRoom, status, startDate, endDate);
            RoomManager roomManager = RoomManager.GetInstance();
            if(Activities.ContainsKey(newActivity.Id) || !roomManager.AddActivity(newActivity.CorrespondingRoom, newActivity.Id))
            {
                return false;
            }

            Activities.Add(newActivity.Id, newActivity);
            SerializationManager.SaveActivities(Activities);
            return true;
        }

        public void EditActivity(string activityId, string category = null, string description = null, ActivityStatus status = ActivityStatus.Undefined, DateTime startDate = default(DateTime), DateTime endDate = default(DateTime))
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

            SerializationManager.SaveActivities(Activities);
        }

        public bool DeleteActivity(string activityId)
        {
            if (!Activities.ContainsKey(activityId))
            {
                return false;
            }

            RoomManager roomManager = RoomManager.GetInstance();
            if(!roomManager.DeleteActivity(Activities[activityId].CorrespondingRoom, activityId))
            {
                return false;
            }

            Activities.Remove(activityId);
            SerializationManager.SaveActivities(Activities);
            return true;
            
        }

        public Dictionary<string, string> GetActivities()
        {
            Dictionary<string, string> activityDictionary = new Dictionary<string, string>();
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
