using System;
using System.Linq.Expressions;
using Microsoft.Win32.SafeHandles;


namespace TrainStation
{
    public class Station
    {
        //common for all trains
        private string id = "";
        private Train.TrainStatus status = 0;
        private int arrivalTime = 0;
        private string type = "";

        //sepecific for passenger trains
        private int numberOfPassengers = 0;
        private int capacity = 0;

        //specific for freight trains 
        private int maxWeight = 0;
        private string freightType = "";

        private List<Platform> Platforms { get; set; }
        private List<Train> Trains { get; set; }

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
            Console.WriteLine("Plarform Status:");
            foreach (Platform platform in Platforms)
            {
                Console.WriteLine(platform.ToString());
            }

            Console.WriteLine("\nTrain Status:");
            foreach (Train train in Trains)
            {
                Console.WriteLine(train.ToString());
            }
        }

        public void LoadFromFile(string path)
        {
            bool validAccess = true;

            StreamReader sr = File.OpenText(path);
            string? line = sr.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    while (line != null)
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
                                //id
                                try
                                {
                                    id = parts[0];
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Invalid ID format.");
                                }

                                //arrival time
                                try
                                {
                                    arrivalTime = Convert.ToInt32(parts[1]);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Invalid time format.");
                                    Console.ReadLine();
                                }

                                //type
                                if (type == "Passengers")
                                {
                                    try
                                    {
                                        numberOfPassengers = int.Parse(parts[3]);
                                        capacity = int.Parse(parts[4]);

                                        Trains.Add(new PassengerTrain(id, status, arrivalTime, type, numberOfPassengers, capacity));
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Invalid Additional Data format.");
                                        Console.ReadLine();
                                    }
                                }
                                if (type == "Freight")
                                {
                                    try
                                    {
                                        maxWeight = int.Parse(parts[3]);
                                        freightType = parts[4];

                                        Trains.Add(new FreightTrain(id, status, arrivalTime, type, maxWeight, freightType));
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("Invalid Additional Data format.");
                                        Console.ReadLine();
                                    }
                                }
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Error, please enter the correct data type, not null.");
                            Console.ReadLine();
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error, please enter the correct data type.");
                            Console.ReadLine();
                        }

                        line = sr.ReadLine(); //read the next line of the file 
                    }

                    Console.WriteLine("Trains loaded successfully.");
                }
                else
                {
                    Console.WriteLine("File not found.");
                    Console.ReadLine();
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
                Console.WriteLine("Click any key to advance a tick");

                AdvanceTick();
            }
        }

        public void AdvanceTick() //advance one tick
        {
            foreach (Train train in Trains)
            {
                arrivalTime = arrivalTime - 15;
                if (arrivalTime <= 0)
                {
                    train.SetArrivalTime(0);
                }
                else
                {
                    train.SetArrivalTime(arrivalTime);
                }
            }

            if (arrivalTime <= 0)
            {
                //find free platform 
                foreach (Platform platform in Platforms)
                {
                    if (platform.GetPlatformStatus() == Platform.PlatformStatus.Free)
                    {
                        //there is free --> docking 
                        foreach (Train train in Trains)
                        {
                            platform.SetCurrentTrain(train);
                            train.SetStatus(Train.TrainStatus.Docking);
                        }
                    }

                    if (platform.GetPlatformStatus() == Platform.PlatformStatus.Ocupied)
                    {
                        //there is no free (occupied) --> waiting
                        status = Train.TrainStatus.Waiting;

                        platform.dockingTime--; //decrease docking time

                        if (platform.dockingTime <= 0) //once docking time is 0 the platform gets free and the train docked
                        {
                            foreach (Train train in Trains)
                            {
                                platform.SetCurrentTrain(train);
                                train.SetStatus(Train.TrainStatus.Docking);

                                //status = Train.TrainStatus.Docked;
                            }

                            platform.SetPlatformStatus(Platform.PlatformStatus.Free); 
                        }
                    }
                }
            }
        }
    }
}

