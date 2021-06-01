using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Store_Monitoring
{
    public static class Parser
    {

        public static List<IEntity> GetCells(String json)
        {
            JArray js = (JArray)JsonConvert.DeserializeObject(json);
            List<IEntity> res = new List<IEntity>();

            foreach (var cell in js)
            {
                var cl = new Cell();
                foreach (JObject item in cell["Items"])
                {
                    if (item["Name"].ToString().Equals("CellNumber"))
                    {
                        cl.Name = item["Value"].ToString();
                    }

                    if (item["Name"].ToString().Equals("Uid"))
                    {
                        cl.Guid = new Guid(item["Value"].ToString());
                    }
                }
                res.Add(cl);
            }
            return res;
        }
    }
}
