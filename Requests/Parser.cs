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
        public static List<IEntity> ParseCells(String json)
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
        public static List<IEntity> ParsePacks(String json)
        {
            JArray js = (JArray)JsonConvert.DeserializeObject(json);
            List<IEntity> res = new List<IEntity>();

            foreach (var pack in js)
            {
                var pck = new Pack();
                foreach (JObject item in pack["Items"])
                {
                    if (item["Name"].ToString().Equals("Name"))
                    {
                        pck.Name = item["Value"].ToString();
                    }

                    if (item["Name"].ToString().Equals("FullName"))
                    {
                        pck.FullName = item["Value"].ToString();
                    }

                    if (item["Name"].ToString().Equals("Uid"))
                    {
                        pck.Guid = new Guid(item["Value"].ToString());
                    }

                    if (item["Name"].ToString().Equals("Artikul"))
                    {
                        pck.Article = item["Value"].ToString();
                    }
                }
                res.Add(pck);
            }
            return res;
        }
        public static List<IEntity> ParsePallets(String json)
        {
            JArray js = (JArray)JsonConvert.DeserializeObject(json);
            List<IEntity> res = new List<IEntity>();

            foreach (var pallet in js)
            {
                var plt = new Pallet();
                foreach (JObject item in pallet["Items"])
                {
                    if (item["Name"].ToString().Equals("PalletNumber"))
                    {
                        plt.Name = item["Value"].ToString();
                    }

                    if (item["Name"].ToString().Equals("Uid"))
                    {
                        plt.Guid = new Guid(item["Value"].ToString());
                    }
                }
                res.Add(plt);
            }
            return res;
        }
    }
}
