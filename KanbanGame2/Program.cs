using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace KanbanGame2
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
            Stream stream = client.OpenRead("http://data.ntpc.gov.tw/api/v1/rest/datastore/382000000A-000352-001");
            StreamReader reader = new StreamReader(stream);
            String content = reader.ReadToEnd();

            //轉成物件
            List<OpenDate> items = JsonConvert.DeserializeObject<List<OpenDate>>(content);
        }
        
        
        public class OpenDate
        {
            public bool success { get; set; }
            public Result result { get; set; }
        }

        public class Result
        {
            public string resource_id { get; set; }
            public int limit { get; set; }
            public int total { get; set; }
            public Field[] fields { get; set; }
            public Record[] records { get; set; }
        }

        public class Field
        {
            public string type { get; set; }
            public string id { get; set; }
        }

        public class Record
        {
            public string sno { get; set; }
            public string sna { get; set; }
            public string tot { get; set; }
            public string sbi { get; set; }
            public string sarea { get; set; }
            public string mday { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
            public string ar { get; set; }
            public string sareaen { get; set; }
            public string snaen { get; set; }
            public string aren { get; set; }
            public string bemp { get; set; }
            public string act { get; set; }
        }

    }
}
