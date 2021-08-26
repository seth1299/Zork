using System;

namespace Zork
{

    enum Commands
    {
        QUIT,
        Q,
        LOOK,
        L,
        NORTH,
        N,
        SOUTH,
        S,
        EAST,
        E,
        WEST,
        W,
        HELP,
        H,
        UNKNOWN
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKNOWN;

            while (command != Commands.QUIT && command != Commands.Q)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString;
                switch (command)
                {

                    case Commands.LOOK:
                    case Commands.L:
                        outputString = "A rubber mat saying \"Welcome to Zork!\" lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.N:
                        outputString = "You moved NORTH.";
                        break;

                    case Commands.SOUTH:
                    case Commands.S:
                        outputString = "You moved SOUTH.";
                        break;

                    case Commands.EAST:
                    case Commands.E:
                        outputString = "You moved EAST.";
                        break;

                    case Commands.WEST:
                    case Commands.W:
                        outputString = "You moved WEST.";
                        break;

                    case Commands.QUIT:
                    case Commands.Q:
                        outputString = "Thanks for playing!";
                        break;

                    case Commands.HELP:
                    case Commands.H:
                        outputString = "Type \"NORTH\" or \"N\" to go NORTH, \"SOUTH\" or \"S\" to go SOUTH, \"WEST\" or \"W\" to go WEST, \"EAST\" or \"E\" to go EAST, \"QUIT\" or \"Q\" to quit the game, or \"L\" or \"LOOK\" to look around your current location.";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

    }
}
