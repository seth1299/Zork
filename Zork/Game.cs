using System.IO;
using System;
using Newtonsoft.Json;

namespace Zork
{
    public class Game
    {
        public World World { get; private set; }

        [JsonIgnore]
        public Player Player { get; private set; }

        [JsonIgnore]
        private bool IsRunning { get; set; }

        public Game(World world, Player player)
        {
            World = world;
            Player = player;
        }

        public void Run()
        {
            IsRunning = true;
            Room previousRoom = null;
            while (IsRunning)
            {
                Console.WriteLine($"\n{Player.Location}"); // This interpolated string with a New Line at the front is intentional to create more "white space" in the program. Having the lines so close to each other made me claustrophobic.
                if (previousRoom != Player.Location)
                {
                    Console.WriteLine(Player.Location.Description);
                    previousRoom = Player.Location;
                }

                Console.Write("\n> ");
                Commands command = ToCommand(Console.ReadLine().Trim());

                IncreaseMoves(command);

                switch (command)
                {
                    case Commands.QUIT:
                        IsRunning = false;
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(Player.Location.Description);
                        break;

                    case Commands.SCORE:
                        switch (Player.Moves) // There's probably a significantly more efficient way to do this one-time grammar check, but this was the most efficient way I could think of.
                        {                     // I would just like to not lose any points for inefficient code I made while trying to work ahead and solve problems ahead of time please.
                            case 1:
                            Console.WriteLine($"Your score is {Player.Score} in {Player.Moves} move.");
                            break;

                            default:
                            Console.WriteLine($"Your score is {Player.Score} in {Player.Moves} moves.");
                            break;
                        }
                        
                        break;

                    case Commands.REWARD:
                        Console.WriteLine("Score increased by 1.");
                        Player.Score++;
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        Directions direction = Enum.Parse<Directions>(command.ToString(), true);
                        if (Player.Move(direction) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        private void IncreaseMoves(Commands command)
        {
            switch(command)
            {
                case Commands.UNKNOWN:
                    break;

                default:
                    Player.Moves++;
                    break;
            }
        }

        public static Game Load(string filename)
        {
            Game game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(filename));

            game.Player = game.World.SpawnPlayer();

            return game;
        }

        private static Commands ToCommand(string commandString) => Enum.TryParse<Commands>(commandString, true, out Commands result) ? result : Commands.UNKNOWN;

    }

}