using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Zork
{

    internal class Program
    {
        private static String CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        private static Commands ConvertCommandShortcutToFullName(Commands command)
        {
            switch (command)
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

                case Commands.I:
                    return Commands.INVENTORY;

                default:
                    return command;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!\n");

            while (true)
            { 
                Console.Write($"{CurrentRoom}\n>");

                Commands command = ConvertCommandShortcutToFullName(ToCommand(Console.ReadLine().Trim()));

                Console.WriteLine("");

                if (command == Commands.QUIT)
                {
                    Console.Write("Thanks for playing!\n");
                    break;
                }


                switch (command)
                {

                    case Commands.LOOK:
                        Console.WriteLine("A rubber mat saying \"Welcome to Zork!\" lies by the door.\n");
                        break;

                    case Commands.INVENTORY:
                        Console.WriteLine("You are carrying a piece of paper with the words \"Up, up, down, down, left, right, left, right, B, A\" written on it. \nYou aren't sure what they mean.\n");
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if ( Move(command) == false )
                        {
                            Console.WriteLine("The way is shut!\n");
                        }
                        break;

                    case Commands.HELP:
                        Console.WriteLine("Type \"NORTH\" or \"N\" to go NORTH, \"SOUTH\" or \"S\" to go SOUTH, \"WEST\" or \"W\" to go WEST, \"EAST\" or \"E\" to go EAST, \"QUIT\" or \"Q\" to " +
                            "quit the game, \"I\" or \"INVENTORY\" to look in your inventory, or \"L\" or \"LOOK\" to look around your current location.\n");
                        break;

                    default:
                        Console.WriteLine("Unknown command.\n");
                        break;
                }
            }
        }

        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            bool didMove = true;

            switch (command)
            {
                case Commands.NORTH when Location.Row < Rooms.GetLength(0) - 1:
                    Location.Row++;
                    break;

                case Commands.SOUTH when Location.Row > 0:
                    Location.Row--;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    didMove = false;
                    break;
            }

            return didMove;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

        private static bool IsDirection(Commands command) => Directions.Contains(command);

        private static readonly string[,] Rooms =
         {
            {"Rocky Trail", "South of House", "Canyon View" },
            {"Forest", "West of House", "Behind House"},
            {"Dense Woods", "North of House", "Clearing"}
         };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);
    }
}