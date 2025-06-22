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

            for (int i = 0; i < numPlatforms; i++) //adds the number of platforms that the user entered
            {
                Platforms.Add(new Platform($"P{i + 1}", Platform.PlatformStatus.Free, null, 2));
            }
        }

        public void DisplayStatus()
        {
            Console.WriteLine("\nPlatform Status:"); //display platform status
            foreach (Platform platform in Platforms)
            {
                platform.DisplayStatus();
            }

            Console.WriteLine("\nTrain Status:"); //displays train status
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
                                if (parts[2] == "Passenger") //adds passenger train
                                {
                                    Trains.Add(new PassengerTrain(parts[0], Convert.ToInt32(parts[1]), parts[2], Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4])));
                                }
                                else if (parts[2] == "Freight") //adds freight train
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

                    Console.WriteLine("Trains loaded successfully."); //loaded all trains 
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

                DisplayStatus(); //display
                AdvanceTick(); //every tick

                simulation = false;

                foreach (Train train in Trains)
                {
                    if (train.GetStatus() != Train.TrainStatus.Docked) //not all trains have been docked so we continue the simulation
                    {
                        simulation = true;
                    }
                }

                if (!simulation) //all the trains are docked 
                {
                    DisplayStatus();
                    Console.WriteLine("\n All trains are docked. Exiting simulation..."); //finish simulation
                    Console.ReadKey();
                }
                
            }
        }

        public void AdvanceTick() //advance one tick
        {
            foreach (Train train in Trains)
            {
                int time = train.GetArrivalTime() - 15; //every tick is -15

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
                        //there has to be a free platform to be able to dock 
                        if (!freePlatform && platform.GetPlatformStatus() == Platform.PlatformStatus.Free)
                        {
                            //once its docking we change the status of the platform to occupied 
                            platform.SetCurrentTrain(train);
                            platform.SetPlatformStatus(Platform.PlatformStatus.Ocupied);
                            platform.SetDockingTime(2);
                            train.SetStatus(Train.TrainStatus.Docking); //changed status to docking
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



