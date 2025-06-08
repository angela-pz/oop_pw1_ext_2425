using System;

namespace TrainStation
{
    public class Platform
    {
        public string id;
        public Train currentTrain;
        public int dockingTime;

        public Platform(string id, int dockingTime)
        {
            this.id = id;
        }
    }
}