using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static KanbanGameConsole.Stations;

namespace KanbanGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DeserializeObject();

            //Do Merge
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

            TableBuilder tb = new TableBuilder();
            tb.AddRow("seq", "車站編號", "車站中文名稱", "車站英文名稱", "車站緯度", "車站經度");
            tb.AddRow("----", "---------", "---------------", "---------------", "---------", "---------");
            foreach (var item in items.data.data)
            {
                //Stations.cs is error code
                //tb.AddRow(item.seq, item.StationId, item.StationChineseName, item.StationEnglishName, item.StationLatitude, item.StationLongitude);

                tb.AddRow(item.seq, item.車站編號, item.車站中文名稱, item.車站英文名稱, item.車站緯度, item.車站經度);
            }
            Console.Write(tb.Output());
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
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

        public interface ITextRow
        {
            String Output();
            void Output(StringBuilder sb);
            Object Tag { get; set; }
        }

        public class TableBuilder : IEnumerable<ITextRow>
        {
            protected class TextRow : List<String>, ITextRow
            {
                protected TableBuilder owner = null;
                public TextRow(TableBuilder Owner)
                {
                    owner = Owner;
                    if (owner == null) throw new ArgumentException("Owner");
                }
                public String Output()
                {
                    StringBuilder sb = new StringBuilder();
                    Output(sb);
                    return sb.ToString();
                }
                public void Output(StringBuilder sb)
                {
                    sb.AppendFormat(owner.FormatString, this.ToArray());
                }
                public Object Tag { get; set; }
            }

            public String Separator { get; set; }

            protected List<ITextRow> rows = new List<ITextRow>();
            protected List<int> colLength = new List<int>();

            public TableBuilder()
            {
                Separator = "  ";
            }

            public TableBuilder(String separator)
                : this()
            {
                Separator = separator;
            }

            public ITextRow AddRow(params object[] cols)
            {
                TextRow row = new TextRow(this);
                foreach (object o in cols)
                {
                    String str = o.ToString().Trim();
                    row.Add(str);
                    if (colLength.Count >= row.Count)
                    {
                        int curLength = colLength[row.Count - 1];
                        if (str.Length > curLength) colLength[row.Count - 1] = str.Length;
                    }
                    else
                    {
                        colLength.Add(str.Length);
                    }
                }
                rows.Add(row);
                return row;
            }

            protected String _fmtString = null;
            public String FormatString
            {
                get
                {
                    if (_fmtString == null)
                    {
                        String format = "";
                        int i = 0;
                        foreach (int len in colLength)
                        {
                            format += String.Format("{{{0},-{1}}}{2}", i++, len, Separator);
                        }
                        format += "\r\n";
                        _fmtString = format;
                    }
                    return _fmtString;
                }
            }

            public String Output()
            {
                StringBuilder sb = new StringBuilder();
                foreach (TextRow row in rows)
                {
                    row.Output(sb);
                }
                return sb.ToString();
            }

            #region IEnumerable Members

            public IEnumerator<ITextRow> GetEnumerator()
            {
                return rows.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return rows.GetEnumerator();
            }

            #endregion
        }

    }
}
