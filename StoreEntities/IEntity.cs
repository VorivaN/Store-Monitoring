using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Monitoring
{
    /// <summary>
    /// Описывает сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Наименование сущности
        /// </summary>
        String Name { get; set; }
        /// <summary>
        /// Уникальный идентификатор сущности
        /// </summary>
        Guid Guid { get; set; }

        ObjectType Type { get; set;  }
    }
    public enum ObjectType
    {
        Pallet, Pack, Cell
    }
}
