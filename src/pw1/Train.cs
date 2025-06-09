using System;
using System.Collections.Specialized;

namespace TrainStation
{
    public class Train
    {
        public string id { get; set; }
        public int arrivalTime { get; set; }
        public string type { get; set; }

        public Train(string id, int arrivalTime, string type)
        {
            this.id = id;
            this.arrivalTime = arrivalTime;
            this.type = type;
        }
    }
}