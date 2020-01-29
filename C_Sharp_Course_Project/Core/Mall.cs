using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core
{
    public class Mall
    {
        [JsonProperty]
        public readonly Guid Id;
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> AssociatedActivities { get; set; }

        public Mall(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            AssociatedActivities = new List<Guid>();
        }

        public bool HasValidData()
        {
            //TODO Implement validation method.
            return true;
        }
    }
}
