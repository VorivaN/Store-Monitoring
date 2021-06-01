using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Monitoring
{
    /// <summary>
    /// Описывает сущность, являющуюся объектом, хранимым на складе
    /// </summary>
    public interface IStoreEntity : IEntity
    {
        /// <summary>
        /// Артикул сущности
        /// </summary>
        String Article { get; set; }
        /// <summary>
        /// Складская ячейка сущности, может быть null
        /// </summary>
        Cell Cell { get; set; }

    }

}
