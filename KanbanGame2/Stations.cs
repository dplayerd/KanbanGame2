using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Stations
{
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public class Rootobject
    {
        private bool _success;
        private object _message;
        private string _id;
        private Data _data;

        public Data data { get => _data; set => _data = value; }
        public string id { get => _id; set => _id = value; }
        public object message { get => _message; set => _message = value; }
        public bool success { get => _success; set => _success = value; }
    }

    public class Data
    {
        private bool _success;
        private object _message;
        private string _id;
        private Datum[] _data;

        public Datum[] data { get => _data; set => _data = value; }
        public string id { get => _id; set => _id = value; }
        public object message { get => _message; set => _message = value; }
        public bool success { get => _success; set => _success = value; }
    }

    public class Datum
    {
        private string _stationId;
        private string _stationChineseName;
        private string _stationEnglishName;
        private string _stationlongitude;
        private string _stationLatitude;

        public int seq { get; set; }
        /// <summary>
        /// 車站編號
        /// </summary>
        public string StationId { get => _stationId; set => _stationId = value; }
        /// <summary>
        /// 車站中文名稱
        /// </summary>
        public string StationChineseName { get => _stationChineseName; set => _stationChineseName = value; }
        /// <summary>
        /// 車站英文名稱
        /// </summary>
        public string StationEnglishName { get => _stationEnglishName; set => _stationEnglishName = value; }
        /// <summary>
        /// 車站緯度
        /// </summary>
        public string StationLatitude { get => _stationLatitude; set => _stationLatitude = value; }
        /// <summary>
        /// 車站經度
        /// </summary>
        public string StationLongitude { get => _stationlongitude; set => _stationlongitude = value; }
    }
}
