using CardGameWar.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using Console = Colorful.Console;
using Colorful;

namespace CardGameWar
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalTurnCount = 0;
            int finiteGameCount = 0;
            double avgTurn = 0;
            String player1Name = "";
            String player2Name = "";

            Console.SetWindowSize(180, 61);

            var font = FigletFont.Load("doom.flf");
            Figlet figlet = new Figlet(font);
            Console.WriteLine(figlet.ToAscii("Jose Rodriguez"), Color.FromArgb(67, 144, 198));
            Console.WriteLine(figlet.ToAscii("Game Studios"), Color.FromArgb(67, 144, 198));
            Console.WriteLine(figlet.ToAscii("Presents..."), Color.FromArgb(67, 144, 198));

            Thread.Sleep(5000);

            Console.Clear();


            Console.WriteAscii("Welcome to the card game of...");

            LoadImage();

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

            Console.Clear();

            

            List<string> menuItems = new List<string>()
            {
                "Play Match",
                "Play Simulated Match",
                "Exit"
            };

            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.WriteAscii("MAIN MENU");

                string selectedMenuItem = MainMenu.drawMainMenu(menuItems);
                if (selectedMenuItem == "Play Match")
                {
                    Console.WriteLine("\nEnter name for Player 1: ");
                    player1Name = Console.ReadLine();
                    Console.WriteLine("\nEnter name for Player 2: ");
                    player2Name = Console.ReadLine();
                    Console.WriteLine("\n" + player1Name + " and " + player2Name + " are ready to go to WAR!");
                    ShowSimplePercentage();
                    Console.Clear();

                    for (int i = 0; i < 1000; i++)
                    {
                        //Create game
                        Game game = new Game(player1Name, player2Name);
                        while (!game.IsEndOfGame())
                        {
                            game.PlayTurn();
                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            Console.Clear();
                        }

                        if (game.TurnCount < 1000)
                        {
                            totalTurnCount += game.TurnCount;
                            finiteGameCount++;
                        }
                    }

                    avgTurn = (double)totalTurnCount / (double)finiteGameCount;

                    Console.WriteLine(finiteGameCount + " finite games with an average of " + Math.Round(avgTurn, 2) + " turns per game.");

                    Console.Read();
                }
                else if (selectedMenuItem == "Play Simulated Match")
                {
                    Console.WriteLine("\nEnter name for Player 1: ");
                    player1Name = Console.ReadLine();
                    Console.WriteLine("\nEnter name for Player 2: ");
                    player2Name = Console.ReadLine();
                    Console.WriteLine("\n" + player1Name + " and " + player2Name + " are ready to go to WAR!");
                    ShowSimplePercentage();
                    Console.Clear();

                    for (int i = 0; i < 1000; i++)
                    {
                        //Create game
                        Game game = new Game(player1Name, player2Name);
                        while (!game.IsEndOfGame())
                        {
                            game.PlayTurn();
                        }

                        if (game.TurnCount < 1000)
                        {
                            totalTurnCount += game.TurnCount;
                            finiteGameCount++;
                        }
                    }

                    avgTurn = (double)totalTurnCount / (double)finiteGameCount;

                    Console.WriteLine(finiteGameCount + " finite games with an average of " + Math.Round(avgTurn, 2) + " turns per game.");

                    Console.Read();
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }           
        }

        private void ExitGame()
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private static void ShowSimplePercentage()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rLoading {i}%   ");
                Thread.Sleep(25);
            }

            Console.Write("\rDone!          ");
        }

        private static void LoadImage()
        {
            Image Picture = Image.FromFile("smallwar.jpg"); 
            Console.SetBufferSize((Picture.Width * 0x2), (Picture.Height * 0x2));
            //Console.WindowWidth = 180;
            //Console.WindowHeight = 61;

            FrameDimension Dimension = new FrameDimension(Picture.FrameDimensionsList[0x0]);
            int FrameCount = Picture.GetFrameCount(Dimension);
            int Left = Console.WindowLeft, Top = Console.WindowTop;
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            Picture.SelectActiveFrame(Dimension, 0x0);
            for (int i = 0x0; i < Picture.Height; i++)
            {
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color Color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    Console.Write(Chars[Index]);
                }
                Console.Write('\n');
                Thread.Sleep(50);
            }
            //Console.SetCursorPosition(Left, Top);
            //Console.Read();
        }

    }
}
