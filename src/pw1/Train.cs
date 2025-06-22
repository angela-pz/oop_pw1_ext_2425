using System;
using System.Runtime.InteropServices;


namespace TrainStation
{
    public abstract class Train
    {
        protected string id;
        protected TrainStatus status;
        protected int arrivalTime;
        protected string type;

        public enum TrainStatus : int
        {
            EnRoute = 1,
            Waiting = 2,
            Docking = 3,
            Docked = 4
        }

        public Train(string id, int arrivalTime, string type)
        {
            this.id = id;
            this.status = TrainStatus.EnRoute;
            this.arrivalTime = arrivalTime;
            this.type = type;
        }

        public string GetId()
        {
            return this.id;
        }

        public TrainStatus GetStatus()
        {
            return this.status;
        }
        public void SetStatus(TrainStatus status)
        {
            this.status = status;
        }
        public int GetArrivalTime()
        {
            return this.arrivalTime;
        }
        public void SetArrivalTime(int ArrivalTime)
        {
            this.arrivalTime = ArrivalTime;
        }
        
        public void DisplayStatus()
        {
            Console.Write($"ID: {id}, Status: {status}, Time for arrival: {arrivalTime}\n");
        }
    }
}