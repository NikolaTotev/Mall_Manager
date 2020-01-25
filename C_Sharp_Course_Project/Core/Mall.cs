using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Mall
    {
        public Guid MallId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> AssociatedActivities { get; set; }
    }
}
