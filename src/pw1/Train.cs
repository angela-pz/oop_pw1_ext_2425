using System;
using System.Collections.Specialized;

namespace TrainStation
{
    public class Train
    {
        public string id { get; set; }
        public int arrivalTime { get; set; }
        public string type { get; set; }

        public Train(string Id, int ArrivalTime, string Type)
        {
            this.id = Id;
            this.arrivalTime = ArrivalTime;
            this.type = type;
        }
    }
}