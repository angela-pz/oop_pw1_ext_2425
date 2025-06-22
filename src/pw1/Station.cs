using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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

                simulation = false;

                foreach (Train train in Trains)
                {
                    if (train.GetStatus() != Train.TrainStatus.Docked)
                    {
                        simulation = true;
                    }
                }

                if (!simulation)
                {
                    DisplayStatus();
                    Console.WriteLine("\n All trains are docked. Exiting simulation...");
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
                    train.SetArrivalTime(time);
                }

                if (time <= 0 && (train.GetStatus() == Train.TrainStatus.EnRoute || train.GetStatus() == Train.TrainStatus.Waiting)) //once arrival time is 0, we start docking
                {
                    //free platform
                    bool freePlatform = false;

                    foreach (Platform platform in Platforms)
                    {
                        if (!freePlatform && platform.GetPlatformStatus() == Platform.PlatformStatus.Free)
                        {
                            platform.SetCurrentTrain(train);
                            platform.SetPlatformStatus(Platform.PlatformStatus.Ocupied);
                            platform.SetDockingTime(2);
                            train.SetStatus(Train.TrainStatus.Docking); //docking
                            freePlatform = true;
                        }
                    }

                    //if its already occuppied we change the status to waiting
                    if (!freePlatform)
                    {
                        train.SetStatus(Train.TrainStatus.Waiting);
                    }
                }
            }

            //if the platform is occupied 
            foreach (Platform platform in Platforms)
            {
                if (platform.GetPlatformStatus() == Platform.PlatformStatus.Ocupied)
                {
                    if (platform.GetCurrentTrain() != null && platform.GetCurrentTrain().GetStatus() == Train.TrainStatus.Docking)
                    {
                        platform.SetDockingTime(platform.GetDockingTime() - 1); //docking time until the train is docked 

                        if (platform.GetDockingTime() <= 0)
                        {
                            platform.GetCurrentTrain().SetStatus(Train.TrainStatus.Docked); //docking time finishes so we can dock the train
                            platform.SetPlatformStatus(Platform.PlatformStatus.Free); //free the platform 
                            platform.SetCurrentTrain(null);
                        }
                    }
                }
            }
        }
    }
}



