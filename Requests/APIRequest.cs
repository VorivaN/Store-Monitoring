using System;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Store_Monitoring
{
    /// <summary>
    /// Класс для создания запросов к базе данных через API системы ELMA
    /// </summary>
    public class APIRequest
    {
        String ServerAddress;
        String Username;
        String Password;
        String ApplicationToken;
        public APIRequest(String ServerAddress, String Username, String Password, String ApplicationToken)
        {
            this.ServerAddress = ServerAddress;
            this.Username = Username;
            this.Password = Password;
            this.ApplicationToken = ApplicationToken;
        }

        /// <summary>
        /// Авторизует пользователя по ApplicationToken, Username и Password
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Получает список сущностей в виде Json из соотсветствующей таблицы
        /// </summary>
        /// <param name="ListUid"></param>
        /// <returns></returns>
        public String GetEntitiesByTableUid(String ListUid)
        {
            WebRequest req = (HttpWebRequest)WebRequest.Create(String.Format(@"http://{0}/API/REST/Entity/QueryTree?type={1}&limit=500", ServerAddress, ListUid));
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
