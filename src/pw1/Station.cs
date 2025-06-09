using System;
using System.ComponentModel.DataAnnotations;

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
            string id = "";
            Train.TrainStatus status = 0;
            int arrivalTime = 0;
            string type = "";
            int numberOfPassengers = 0;
            int capacity = 0;
            int maxWeight = 0;
            string freightType = "";

            bool validAccess = true;

            try
            {
                if (File.Exists(path))
                {
                    StreamReader sr = File.OpenText(path);
                    string? line = sr.ReadLine();
                    line = sr.ReadLine();

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
                                catch
                                {
                                    Console.WriteLine("Invalid ID format.");
                                }

                                //arrival time
                                try
                                {
                                    arrivalTime = Convert.ToInt32(parts[1]);
                                }
                                catch
                                {
                                    Console.WriteLine("Invalid distance format.");
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
                                    catch
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
                                    catch
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

                    sr.Close(); //close the file after reading it
                }
                else
                {
                    Console.WriteLine("File not found.");
                    Console.ReadLine();
                    validAccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error loading file: " + ex.Message);
                Console.ReadLine();
                validAccess = false;
            }
            return validAccess;
        }


        public void StartSimulation()
        {
            Console.WriteLine("How many platforms are there?: ");
            
        }

    }
}

