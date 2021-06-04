using System;


namespace Store_Monitoring
{
    /// <summary>
    /// Упаковка
    /// </summary>
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
