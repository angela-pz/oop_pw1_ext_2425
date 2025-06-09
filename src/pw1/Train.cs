using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace TrainStation
{
    public class Train
    {
        protected string id { get; set; }
        protected TrainStatus status { get; set; }
        protected int arrivalTime { get; set; }
        protected string type { get; set; }

        public enum TrainStatus : int
        {
            EnRoute = 1,
            Waiting = 2,
            Docking = 3,
            Docked = 4
        }

        public Train(string id, TrainStatus status, int arrivalTime, string type)
        {
            this.id = id;
            this.status = status;
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

        public void AvanceTick() //virtual?
        {
            if (status == Train.TrainStatus.EnRoute)
            {
                // Calculate time in 15 minutes (time / 4)
                arrivalTime = arrivalTime / 4;

                // When the time gets to 0, the trains goes to waiting 
                if (arrivalTime == 0)
                {
                    status = Train.TrainStatus.Waiting;
                }
            }
        }
        
        public virtual void ShowStatus()
        {
            Console.Write($" ID: {id}, Status: {status}, Time for arrival: {arrivalTime}");
        }
    }
}