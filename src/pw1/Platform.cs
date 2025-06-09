using System;
using System.Runtime.CompilerServices;

namespace TrainStation
{
    public class Platform
    {
        public string id { get; set; }
        public PlatformStatus status { get; set; }
        public Train currentTrain { get; set; }
        public int dockingTime { get; set; }
        public int ticksRemaining { get; set; }
        public const int deafaultTicks = 2;
        public enum PlatformStatus
        {
            Free,
            Ocupied
        }

        public Platform(string id, PlatformStatus status, Train currentTrain, int dockingTime, int ticksRemaining)
        {
            this.id = id;
            this.status = PlatformStatus.Free;
            this.currentTrain = null;
            this.dockingTime = 0;
            this.ticksRemaining = ticksRemaining;
        }

        //getter for status 

        public bool RequestPlatform(Train train)
        {
            if (status == PlatformStatus.Free && train.GetStatus() == Train.TrainStatus.Waiting)
            {
                currentTrain = train;
                status = PlatformStatus.Ocupied;
                ticksRemaining = deafaultTicks;
                train.SetStatus(Train.TrainStatus.Docking);
                return true;
            }
            return false;
        }

        public void FreeRunway()
        {
            currentTrain = null;
            status = PlatformStatus.Free;
            ticksRemaining = 0;
        }

        public void DisplayStatus()
        {
            if (status == PlatformStatus.Free)
            {
                Console.WriteLine($"{id}: Free");
            }
            else
            {
                Console.WriteLine($"{id}: Occupied by {currentTrain.GetId()}, Ticks Remaining: {ticksRemaining}");
            }
        }
    }
}