using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            DeserializeObject();
        }

        public static void DeserializeObject()
        {
            //JSON字串
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://api.kcg.gov.tw/api/service/get/4278fc6a-c3ea-4192-8ce0-40f00cdb40dd");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            //轉成物件
            Rootobject items = JsonConvert.DeserializeObject<Rootobject>(content);


        }
     


        public class Rootobject
        {
            public Data data { get; set; }
            public string id { get; set; }
            public object message { get; set; }
            public bool success { get; set; }
        }

        public class Data
        {
            public Datum[] data { get; set; }
            public string id { get; set; }
            public object message { get; set; }
            public bool success { get; set; }
        }

        public class Datum
        {
            public int seq { get; set; }
            public string 車站編號 { get; set; }
            public string 車站中文名稱 { get; set; }
            public string 車站英文名稱 { get; set; }
            public string 車站緯度 { get; set; }
            public string 車站經度 { get; set; }
        }
    }
}
