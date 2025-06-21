using System;
using System.Runtime.InteropServices;


namespace TrainStation
{
    public abstract class Train
    {
        protected string ID;
        protected TrainStatus Status;
        protected int ArrivalTime;
        protected string Type;

        public enum TrainStatus : int
        {
            EnRoute = 1,
            Waiting = 2,
            Docking = 3,
            Docked = 4
        }

        public Train(string ID, int ArrivalTime, string Type)
        {
            this.ID = ID;
            this.Status = TrainStatus.EnRoute;
            this.ArrivalTime = ArrivalTime;
            this.Type = Type;
        }

        public string GetId()
        {
            return this.ID;
        }

        public TrainStatus GetStatus()
        {
            return this.Status;
        }
        public void SetStatus(TrainStatus status)
        {
            this.Status = status;
        }
        public int GetArrivalTime()
        {
            return this.ArrivalTime;
        }
        public void SetArrivalTime(int ArrivalTime)
        {
            this.ArrivalTime = ArrivalTime;
        }
        
        public virtual void DisplayStatus()
        {
            Console.Write($" ID: {ID}, Status: {Status}, Time for arrival: {ArrivalTime}");
        }
    }
}