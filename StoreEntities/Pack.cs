using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Monitoring
{
    public class Pack : IStoreEntity
    {
        public Pack()
        {
            this.Type = ObjectType.Pack;
        }
        public string Article { get; set; }
        public Cell Cell { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public Guid Guid { get; set; }
        public ObjectType Type { get; set; }
    }
}
