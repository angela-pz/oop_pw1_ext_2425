using System;

namespace TrainStation
{
    public class FreightTrain : Train
    {
        public int maxWeight { get; set; }
        public string freightType { get; set; }

        public FreightTrain(string id, int arrivalTime, string type, int maxWeight, string freightType) : base(id, arrivalTime, type)
        {
            this.maxWeight = maxWeight;
            this.freightType = freightType;
        }
    }
}