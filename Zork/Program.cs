using System;

namespace Zork
{

    class Program
    {

        private static string Location => Rooms[LocationColumn];

        private static Commands ConvertCommandShortcutToFullName(Commands command)
        {
            switch(command)
            {
                case Commands.N:
                    return Commands.NORTH;

                case Commands.S:
                    return Commands.SOUTH;

                case Commands.E:
                    return Commands.EAST;

                case Commands.W:
                    return Commands.WEST;

                case Commands.L:
                    return Commands.LOOK;

                case Commands.Q:
                    return Commands.QUIT;

                case Commands.H:
                    return Commands.HELP;

                default:
                    return command;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!\n");

            while (true)
            {
                string outputString;

                Console.Write($"{Location}\n>");

                Commands command = ConvertCommandShortcutToFullName(ToCommand(Console.ReadLine().Trim()));

                if (command == Commands.QUIT)
                {
                    Console.Write("Thanks for playing!\n");
                    break;
                }

                
                switch (command)
                {

                    case Commands.LOOK:
                        outputString = "A rubber mat saying \"Welcome to Zork!\" lies by the door.";
                        break;

                    case Commands.NORTH:
<<<<<<< Updated upstream
                    case Commands.N:
                    case Commands.SOUTH:
                    case Commands.S:
                    case Commands.EAST:
                    case Commands.E:
                    case Commands.WEST:
                    case Commands.W:
                        outputString = $"You moved {command.ToString()}.";
                        break;
=======
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
>>>>>>> Stashed changes

                        outputString = Move(command) ? $"You moved {command}." : "The way is shut!";
                        break;

                    case Commands.HELP:
                        outputString = "Type \"NORTH\" or \"N\" to go NORTH, \"SOUTH\" or \"S\" to go SOUTH, \"WEST\" or \"W\" to go WEST, \"EAST\" or \"E\" to go EAST, \"QUIT\" or \"Q\" to " +
                            "quit " +  "the game, or \"L\" or \"LOOK\" to look around your current location.";
                        break;

                    default:
                        outputString = "Unknown command.";
                        break;
                }

                Console.WriteLine(outputString + "\n");
            }
        }

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH:
                case Commands.SOUTH:
                    break;

                case Commands.EAST when LocationColumn < Rooms.Length - 1:
                        LocationColumn++;
                        didMove = true;
                    break;

                case Commands.WEST when LocationColumn > 0:
                        LocationColumn--;
                        didMove = true;
                    break;
            }

            return didMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static string[] Rooms = { "Forest", "West of House", "Behind Hill", "Clearing", "Canyon View" };
        private static int LocationColumn = 1;
    }
}