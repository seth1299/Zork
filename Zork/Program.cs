using System;

namespace Zork
{

    enum Commands
    {
        QUIT,
        LOOK,
        NORTH,
        SOUTH,
        EAST,
        WEST,
        UNKNOWN
    }

    class Program
    {
        static void Main(string[] args)
        {
            string inputString = "";
            Commands command = UNKNOWN;

            Console.WriteLine("Welcome to Zork!");

            inputString = Console.ReadLine();
            command = ToCommand(inputString.Trim().ToUpper());
            Console.WriteLine(command);

            if (inputString == "LOOK" || inputString == "L")
            {
                Console.WriteLine("This is an open field west of a white house, with a boarded front door. \nA rubber mat saying \"Welcome to Zork!\" lies by the door.");
            }
            else
            {
                Console.WriteLine("Unexpected command.");
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

    }
}
