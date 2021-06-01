using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Store_Monitoring
{
    public class APIRequest
    {
        String ServerAddress;
        String CellsUid;
        String PacksUid;
        String PalletsUid;
        String Username;
        String Password;
        String ApplicationToken;
        public APIRequest(String ServerAddress, String Username, String Password, String ApplicationToken, String CellsUid, String PacksUid, String PalletsUid)
        {
            this.ServerAddress = ServerAddress;
            this.CellsUid = CellsUid;
            this.PacksUid = PacksUid;
            this.PalletsUid = PalletsUid;
            this.Username = Username;
            this.Password = Password;
            this.ApplicationToken = ApplicationToken;
        }

        private String Authorize()
        {
            var req = (HttpWebRequest)WebRequest.Create(String.Format(@"http://{0}/API/REST/Authorization/LoginWith?username={1}", ServerAddress, Username));
            req.Accept = "application/json";
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers["ApplicationToken"] = ApplicationToken;

            string json = $"\"{Password}\"";
            var encoding = new UTF8Encoding();
            Byte[] bytes = encoding.GetBytes(json);

            using (Stream newStream = req.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
            }

            String respStr = String.Empty;
            using (WebResponse response = req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        respStr = sr.ReadToEnd();
                        sr.Close();
                    }
                    stream.Close();
                }
                response.Close();
            }

            JObject obj = (JObject)JsonConvert.DeserializeObject(respStr);
            return obj["AuthToken"].ToString();
        }

        public String GetCells()
        {
            WebRequest req = (HttpWebRequest)WebRequest.Create(String.Format(@"http://{0}/API/REST/Entity/QueryTree?type={1}", ServerAddress, CellsUid));
            req.Method = "GET";
            req.Headers.Add("ApplicationToken", ApplicationToken);
            req.Headers["AuthToken"] = Authorize();

            String respStr = String.Empty;
            using (WebResponse response = req.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        respStr = sr.ReadToEnd();
                        sr.Close();
                    }
                    stream.Close();
                }
                response.Close();
            }

            return respStr;
        }


    }
}
