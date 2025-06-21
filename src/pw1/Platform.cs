using System;


namespace TrainStation
{
    public class Platform
    {
        public string id { get; set; }
        public PlatformStatus status { get; set; }
        public Train currentTrain { get; set; }
        public const int dockingTime = 2;

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