using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Monitoring
{
    public class Cell : IEntity
    {
        public Cell()
        {
            this.Type = ObjectType.Cell;
        }


        public string Name { get; set; }
        public Guid Guid { get; set; }
        public ObjectType Type { get; set; }
        public List<IStoreEntity> StoreObjects { get; set; }
    }
}
