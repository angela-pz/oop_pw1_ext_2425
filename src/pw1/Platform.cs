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

        public enum PlatformStatus
        {
            Free,
            Ocupied
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
        public Train GetCurrentTrain()
        {
            return this.currentTrain;
        }
        public void SetCurrentTrain(Train currentTrain)
        {
            this.currentTrain = currentTrain;
        }
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
                Console.WriteLine($"{id}: Occupied by {currentTrain.GetId()}");
            }
        }
    }
}

/*
        public bool RequestPlatform(Train train)
        {
            //if the platform is free and the train is is waiting, the train goes to occupy the platform and the status 
            //of the platform status changes to occupied and the train to the next status (docking)
            if (status == PlatformStatus.Free && train.GetStatus() == Train.TrainStatus.Waiting)
            {
                currentTrain = train;
                status = PlatformStatus.Ocupied;
                train.SetStatus(Train.TrainStatus.Docking);
                return true;
            }
            return false;
        }

        public void FreeRunway()
        {
            currentTrain = null;
            status = PlatformStatus.Free;
        }
*/