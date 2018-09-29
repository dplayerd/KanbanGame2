using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanGameConsole
{
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
