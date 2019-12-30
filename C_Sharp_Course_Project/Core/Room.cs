using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Room
    {
        private string m_Name;
        private string m_Description;
        private string m_Type;
        public readonly Guid ID;
        public DateTime CreateDate { get; private set; }
        public DateTime LastEditDate { get; private set; }

        public string Name
        {
            get => m_Name;
            set
            {
                if (value == null)
                {
                    m_Name = string.Empty;
                }
                else
                {
                    m_Name = value;
                }
            }
        }

        public string Description
        {
            get => m_Description;
            set
            {
                if (value == null)
                {
                    m_Description = string.Empty;
                }
                else
                {
                    m_Description = value;
                }
            }
        }

        public string Type
        {
            get => m_Type;
            set
            {
                if (value == null)
                {
                    m_Type = string.Empty;
                }
                else
                {
                    m_Type = value;
                }
            }
        }

        public Room(string name, string description, string id, DateTime createDate, DateTime lastEditDate)
        {
            Name = name;
            Description = description;
            ID = id == null ? Guid.NewGuid() : Guid.Parse(id);
            CreateDate = createDate;
            LastEditDate = lastEditDate;
        }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            ID = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
            LastEditDate = DateTime.UtcNow;
        }

        public void EditRoom(string name = null, string description = null, string type = null)
        {
            LastEditDate = DateTime.UtcNow;
            if (name != null)
            {
                m_Name = name;
            }
            if (description != null)
            {
                m_Description = description;
            }
            if (type != null)
            {
                m_Type = type;
            }
        }
        //TODO constructors, and methods for editing rooms and deleting, and list of activities for this room
    }
}
