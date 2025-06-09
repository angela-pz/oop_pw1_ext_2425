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

        public void ShowStatus()
        {
            Console.WriteLine("Plarform Status:");
            foreach (var platform in Platforms)
            {
                Console.WriteLine(platform.ToString());
            }

            Console.WriteLine("\nTrain Status:");
            foreach (var train in Trains)
            {
                Console.WriteLine(train.ToString());
            }
        }

        public bool LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return false;
            }

            try
            {
                string[] lines = File.ReadAllLines(path);

                foreach (string line in lines)
                {
                    //id,arrivalTime,type,additionalData1,additionalData2
                    string[] parts = line.Split(',');
                    if (parts.Length < 5)
                    {
                        Console.WriteLine("Error");
                        return false;
                    }
                    string id = parts[0];
                    int arrivalTime = int.Parse(parts[1]);
                    string type = parts[2];

                    if (type.Equals("Passengers", StringComparison.OrdinalIgnoreCase))
                    {
                        int numberOfPassengers = int.Parse(parts[3]);
                        int capacity = int.Parse(parts[4]);

                        Trains.Add(new PassengerTrain(id, arrivalTime, type, numberOfPassengers, capacity));
                    }

                    if (type.Equals("Freight", StringComparison.OrdinalIgnoreCase))
                    {
                        int maxWeight = int.Parse(parts[3]);
                        string freightType = parts[4];

                        Trains.Add(new FreightTrain(id, arrivalTime, type, maxWeight, freightType));
                    }
                    else
                    {
                        Console.WriteLine("Unknown aircraft type");
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                return false;
            }
            return true;
        }

        public void StartSimulation()
        {

        }

    }
}