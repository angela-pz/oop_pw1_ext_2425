using System;

namespace TrainStation
{
    public class PassengerTrain : Train
    {
        public int numberOfPassengers { get; set; }
        public int capacity { get; set; }

        public PassengerTrain(string id, int arrivalTime, string type, int numberOfPassengers, int capacity) : base(id,arrivalTime, type)
        {
            this.numberOfPassengers = numberOfPassengers;
            this.capacity = capacity;
        }
    }
}