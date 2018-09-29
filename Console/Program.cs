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
    // 1.宣告 delegate -- 定義簽章:
    delegate double MathAction(double num);

    public class Program
    {

        static void Main(string[] args)
        {
            SayXmas();

            DeserializeObject();
            testdelegate();
            testaction();
            testaction2();
            testFunc();
        }

        private static void SayXmas()
        {
            Holiday _holiday= new Holiday();
            Action<string> action2 = new Action<string>(MerryXmas);
            action2(_holiday.SayXmas());
        }
        static void MerryXmas(string s)
        {
            Console.WriteLine(s);
        }
        protected static DateTime GetToday()
        {
            return DateTime.Today;
        }

        //Func必須傳回值
        private static void testFunc()
        {
            //藉由Func<T,TResult>委派執行實體化
            Func<int, string> convertMethod = UppercaseString;
            Console.WriteLine(convertMethod(122233));

            Func<int, int, int> func = new Func<int, int, int>(Add);
            int res = func(100, 200);
            Console.WriteLine(res);
        }
        private static string UppercaseString(int inputString)
        {
            return inputString.ToString();
        }
        private static int Add(int x, int y)
        {
            return x = y;
        }


        //Action無回傳值
        private static void testaction()
        {//此範例脫褲子放屁，根本不用Action
            Name testName = new Name("Koani");

            //2.實體化Action並指派方法
            Action showMethod = testName.DisplayToConsole;
            showMethod();
        }
        public class Name
        {
            private string instanceName;
            public Name(string name)
            {
                this.instanceName = name;
            }
            public void DisplayToConsole()
            {
                Console.WriteLine(this.instanceName);
            }
            //1.建立沒有回傳值的方法
            public void DisplayToWindow()
            {
                Console.WriteLine(this.instanceName);
            }
        }

        private static void testaction2()
        {
            Action action = new Action(M1);
            action();

            Action<string> action2 = new Action<string>(SayHello);
            action2("Riva");

            Action<string, int> action3 = new Action<string, int>(SayHello);
            action3("Riva", 3);
        }
        static void M1()
        {
            Console.WriteLine("M1 is called.");
        }
        static void SayHello(string name)
        {
            Console.WriteLine("I'm {0}", name); //Console.WriteLine($"I'm {name}"); C# 6
        }
        static void SayHello(string name, int round)
        {
            for (int i = 0; i < round; i++)
            {
                Console.WriteLine("Hello, {0}", name);
            }
        }



        //delegate
        static double Double(double input)
        {// 2.一般的方法跟delegate的簽章相符:
            return input * 2;
        }

        private static void testdelegate()
        {
            // 3.藉由方法名稱 Instantiate delegate
            MathAction ma = Double;

            // 4.呼叫 delegate ma
            double multByTwo = ma(4.5);
            Console.WriteLine("multByTwo: {0}", multByTwo);

            // 3.藉由匿名方法 Instantiate delegate
            MathAction ma2 = delegate (double input)
            {
                return input * input;
            };
            // 4.呼叫 delegate square
            double square = ma2(5);
            Console.WriteLine("square: {0}", square);

            // 3.藉由lambda expression Instantiate delegate
            MathAction ma3 = s => s * s * s;
            // 4.呼叫 delegate cube
            double cube = ma3(4.375);
            Console.WriteLine("cube: {0}", cube);
            // 輸出:
            // multByTwo: 9
            // square: 25
            // cube: 83.740234375
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





    }
}
