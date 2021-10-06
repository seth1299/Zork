using System;
using System.Collections.Generic;
using System.IO;

namespace Zork
{

    internal class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }

        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap.Add(room.Name, room);
            }
        }

        private enum Fields
        {
            Name = 0,
            Description
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            const string defaultRoomsFileName = "Rooms.txt";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFileName);

            InitializeRoomDescriptions(roomsFilename);

            Room previousRoom = null;

            Commands command = Commands.UNKNOWN;

            while ( command != Commands.QUIT )
            {
                Console.Write($"\n{CurrentRoom}\n");

                if (previousRoom != CurrentRoom)
                {
                    Console.Write(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("\n>");

                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                    
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }

            Console.Write("Thanks for playing!\n");

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

        private static readonly Room[,] Rooms =
         {
            {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },
            {new Room("Forest"), new Room("West of House"), new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing")}
         };

        private static void InitializeRoomDescriptions(string roomsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            string[] lines = File.ReadAllLines(roomsFilename);
            foreach (string line in lines)
            {
                string[] fields = line.Split(fieldDelimiter);
                if (fields.Length != expectedFieldCount)
                {
                    throw new InvalidDataException("Invalid record.");
                }

                string name = fields[(int)Fields.Name];
                string description = fields[(int)Fields.Description];

                RoomMap[name].Description = description;
            }
        }

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);

        private static readonly Dictionary<string, Room> RoomMap;
    }
}