using System;
using System.Collections.Generic;


namespace Store_Monitoring
{
    /// <summary>
    /// Паллет
    /// </summary>
    public class Pallet : IStoreEntity
    {

        public Pallet()
        {
            this.Type = ObjectType.Pallet;
        }

        public string Article { get; set; }
        public Cell Cell { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public ObjectType Type { get; set; }

        public List<Pack> PalletStoreObjects;
    }
}
