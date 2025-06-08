using System;

namespace TrainStation
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("invalid option");
                    break;
            }

        }
    }
}
