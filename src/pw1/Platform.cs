using System;
using System.Runtime.InteropServices;


namespace TrainStation
{
    public class Platform
    {
        public string id; 
        public PlatformStatus status; 
        public Train currentTrain; 
        public int dockingTime = 2;

        public enum PlatformStatus : int
        {
            Free = 0,
            Ocupied = 1
        }

        public Platform(string id, PlatformStatus status, Train currentTrain, int dockingTime)
        {
            this.id = id;
            this.status = PlatformStatus.Free;
            this.currentTrain = null;
        }

        //getter for status 
        public PlatformStatus GetPlatformStatus()
        {
            return this.status;
        }
        public void SetPlatformStatus(PlatformStatus status)
        {
            this.status = status; 
        }

        //current train 
        public Train GetCurrentTrain()
        {
            return this.currentTrain;
        }
        public void SetCurrentTrain(Train currentTrain)
        {
            this.currentTrain = currentTrain;
        }

        //docking time
        public int GetDockingTime()
        {
            return this.dockingTime;
        }
        public void SetDockingTime(int dockingTime)
        {
            this.dockingTime = dockingTime;
        }


        public void DisplayStatus()
        {
            if (status == PlatformStatus.Free)
            {
                Console.WriteLine($"{id}: Free");
            }
            else
            {
                Console.WriteLine($"{id}: Occupied by {currentTrain.GetId()}, Ticks left: {GetDockingTime()}");
            }
        }
    }
}
