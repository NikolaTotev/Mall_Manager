using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastEditDate { get; set; }
        public string Type { get; set; }

        //TODO constructors, and methods for editing rooms and deleting, and list of activities for this room
    }
}
 