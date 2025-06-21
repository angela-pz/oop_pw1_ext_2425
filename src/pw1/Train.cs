using System;


namespace TrainStation
{
    public abstract class Train
    {
        protected string ID { get; set; }
        protected TrainStatus Status { get; set; }
        protected int ArrivalTime { get; set; }
        protected string Type { get; set; }

        public enum TrainStatus : int
        {
            EnRoute = 1,
            Waiting = 2,
            Docking = 3,
            Docked = 4
        }

        public Train(string ID, TrainStatus Status, int ArrivalTime, string Type)
        {
            this.ID = ID;
            this.Status = Status;
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
        
        public virtual void DisplayStatus()
        {
            Console.Write($" ID: {ID}, Status: {Status}, Time for arrival: {ArrivalTime}");
        }
    }
}