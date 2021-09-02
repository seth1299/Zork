﻿using System;

namespace Zork
{

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
                    case Commands.SOUTH:
                    case Commands.S:
                    case Commands.EAST:
                    case Commands.E:
                    case Commands.WEST:
                    case Commands.W:
                        outputString = $"You moved {command.ToString()}.";
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