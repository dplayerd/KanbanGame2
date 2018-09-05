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
            string json = reader.ReadToEnd();

            //轉成物件
            List<OpenDate> items = JsonConvert.DeserializeObject<List<OpenDate>>(json);        }

        public class OpenDate
        {

        }
    }
}
