using System;
using System.Linq.Expressions;
using Microsoft.Win32.SafeHandles;


namespace TrainStation
{
    public class Station
    {
        private List<Platform> Platforms;
        private List<Train> Trains;

        public Station(int numPlatforms)
        {
            this.Platforms = new List<Platform>();
            this.Trains = new List<Train>();

            for (int i = 0; i < numPlatforms; i++)
            {
                Platforms.Add(new Platform($"P{i + 1}", Platform.PlatformStatus.Free, null, 2));
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\nPlatform Status:");
            foreach (Platform platform in Platforms)
            {
                platform.DisplayStatus();
            }

            Console.WriteLine("\nTrain Status:");
            foreach (Train train in Trains)
            {
                train.DisplayStatus();
            }
        }

        public void LoadFromFile(string path)
        {
            StreamReader sr = File.OpenText(path);
            string? line = sr.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            //id,arrivalTime,type,additionalData1,additionalData2
                            string[] parts = line.Split(',');
                            if (parts.Length < 5)
                            {
                                Console.WriteLine("File Format Error.");
                                Console.ReadLine();
                            }
                            else
                            {
                                if (parts[2] == "Passenger")
                                {
                                    Trains.Add(new PassengerTrain(parts[0], Convert.ToInt32(parts[1]), parts[2], Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4])));
                                }
                                else if (parts[2] == "Freight")
                                {
                                    Trains.Add(new FreightTrain(parts[0], Convert.ToInt32(parts[1]), parts[2], Convert.ToInt32(parts[3]), parts[4]));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error regarding to the file. Error: " + ex.Message);
                            Console.ReadLine();
                        }
                    }

                    Console.WriteLine("Trains loaded successfully.");
                    Console.ReadKey();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error regarding to the file. Error: " + ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sr.Close();
            }
        }


        public void StartSimulation()
        {
            bool simulation = true;
            while (simulation)
            {
                Console.WriteLine("\n\nClick any key to advance a tick");
                Console.ReadKey();

                DisplayStatus();

                AdvanceTick();

                foreach (Train train in Trains)
                {
                    if (train.GetStatus() != Train.TrainStatus.Docked)
                    {
                        simulation = true;
                    }
                    else
                    {
                        simulation = false;
                    }
                }

                if (!simulation)
                {
                    Console.WriteLine("All trains are docked. Exiting simulation...");
                    Console.ReadKey();
                }
            }
        }

        public void AdvanceTick() //advance one tick
        {
            foreach (Train train in Trains)
            {
                int time = train.GetArrivalTime() - 15;

                if (time <= 0)
                {
                    train.SetArrivalTime(0);
                }
                else
                {
                    train.SetArrivalTime(train.GetArrivalTime() - 15);
                }

                if (time <= 0)
                {
                    //find free platform 
                    foreach (Platform platform in Platforms)
                    {
                        if (platform.GetPlatformStatus() == Platform.PlatformStatus.Free)
                        {
                            //there is free --> docking 
                            platform.SetCurrentTrain(train);
                            train.SetStatus(Train.TrainStatus.Docking);
                            platform.SetPlatformStatus(Platform.PlatformStatus.Ocupied);
                        }

                        if (platform.GetPlatformStatus() == Platform.PlatformStatus.Ocupied)
                        {
                            //there is no free (occupied) --> waiting
                            train.SetStatus(Train.TrainStatus.Waiting);

                            platform.SetDockingTime(platform.GetDockingTime() - 1); //decrease docking time

                            if (platform.GetDockingTime() <= 0) //once docking time is 0 the platform gets free and the train docked
                            {
                                platform.SetCurrentTrain(train);
                                train.SetStatus(Train.TrainStatus.Docked);

                                platform.SetPlatformStatus(Platform.PlatformStatus.Free);

                                platform.SetCurrentTrain(null);
                            }
                        }
                    }
                }
            }
        }
    }
}

