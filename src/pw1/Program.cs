using System;

namespace TrainStation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //main menu 
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║    Train Station Simulation Menu      ║");
            Console.WriteLine("║                                       ║");
            Console.WriteLine("║ 1. Load trains from file              ║");
            Console.WriteLine("║ 2. Start simulation                   ║");
            Console.WriteLine("║ 3. Exit                               ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.Write("Select an option: ");

            int option = Convert.ToInt32(Console.ReadLine()); //the user selects an option

            Station station = new Station(); // create the object 

            switch (option) //depending on the option selected, it will be directed to the corresponding program
            {
                case 1:
                    Console.Write("Enter file path: "); //the user enters the file path
                    string path = Console.ReadLine();

                    bool result = station.LoadFromFile(path); //call LoadFromFile
                    if (result) Console.WriteLine("Flights loaded successfully.");
                    break;
                case 2:
                    station.StartSimulation(); //cals StartSimulation
                    break;
                case 3:
                    break; //exits
                default:
                    Console.WriteLine("invalid option");
                    break;
            }

        }
    }
}
