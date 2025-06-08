using System;

namespace TrainStation
{
    public class Station
    {
        public List<Platform> Platforms { get; set; }
        public List<Train> Trains { get; set; }

        public Station()
        {
            Platforms = new List<Platform>();
            Trains = new List<Train>();
        }

    }
}