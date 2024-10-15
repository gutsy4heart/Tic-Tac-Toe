using System;
using System.Threading;

namespace TicTacToe
{
    class Game
    {
        char[,] board = new char[3, 3];
        string player1Name = "";
        string player2Name = "";
        bool playerTurn = true;
        char choice;

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;

            Game game = new Game();

            Console.WriteLine("\t\t\t\t\t\t\t\t\t\tДобро пожаловать!");
            Console.WriteLine("\t\t\t\t\t\t\t\t\t\tНажмите на ввод для продолжения...");
            #region Beep
            Console.Beep(659, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(523, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(375);
            Console.Beep(392, 125);
            Thread.Sleep(375);
            #endregion
            Console.ReadKey();
            Console.Clear();
            Console.Write("Х или 0?: ");
            game.choice = char.Parse(Console.ReadLine());

            if (game.choice == 'X')
            {
                Console.Write($"Введите ник первого игрока ({game.choice}): ");
                game.player1Name = Console.ReadLine();
                Console.Write("Введите ник второго игрока (O) или оставьте пустым для игры против компьютера: ");
                game.player2Name = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Загрузка...");
                Thread.Sleep(400);

            }
            else if (game.choice == '0')
            {
                Console.Write($"Введите ник первого игрока ({game.choice}): ");
                game.player1Name = Console.ReadLine();
                Console.Write("Введите ник второго игрока (X) или оставьте пустым для игры против компьютера: ");
                game.player2Name = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("Загрузка...");
                Thread.Sleep(400);

            }

            game.InitializeBoard();

            // Создаем новый поток для звукового сигнала
            Thread beepThread = new Thread(game.BeepSound);
            beepThread.IsBackground = true;
            beepThread.Start();

            while (!game.CheckForWin() && !game.CheckForDraw())
            {
                game.DrawBoard();
                if (game.playerTurn)
                {
                    game.PlayerMove(game.player1Name, game.choice);
                }
                else
                {
                    if (game.player2Name == "")
                    {
                        game.ComputerMove();
                    }
                    else
                    {
                        game.PlayerMove(game.player2Name, game.choice);
                    }
                }
                game.playerTurn = !game.playerTurn;
            }
            game.DrawBoard();
            if (game.CheckForWin())
            {
                Console.WriteLine(game.playerTurn ? $"Игрок {game.player1Name} победил!" : $"Игрок {game.player2Name} победил!");
                
            }
            else
            {
                Console.WriteLine("Ничья!");
                game.BamBamBam();

            }
        }

        void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }

        void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != ' ' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (board[0, i] != ' ' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            if (board[0, 0] != ' ' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] != ' ' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        bool CheckForDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void PlayerMove(string playerName, char playerSymbol)
        {
            Console.WriteLine($"Ход игрока {playerName} ({playerSymbol}):");
            Console.Write("Введите строку: ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Введите столбец: ");
            int col = int.Parse(Console.ReadLine());

            if (row < 0 || row > 2 || col < 0 || col > 2 || board[row, col] != ' ')
            {
                Console.WriteLine("Некорректный ход. Попробуйте снова.");
                PlayerMove(playerName, playerSymbol);
            }
            else
            {
                board[row, col] = playerSymbol;
            }
        }

        void ComputerMove()
        {
            Console.WriteLine("Ход компьютера:");
            Console.Beep(600, 200);
            Random rand = new Random();
            int row, col;
            do
            {
                row = rand.Next(0, 3);
                col = rand.Next(0, 3);
            } while (board[row, col] != ' ');

            board[row, col] = 'O';
        }


        void BamBamBam()
        {
            Console.Beep(500, 200);
            Thread.Sleep(200);
            Console.Beep(500, 200);
            Thread.Sleep(200);
            Console.Beep(500, 200);
        }


        void BeepSound()
        {
            while (true)
            {
                Console.Beep(500, 1000);
                Thread.Sleep(1000);
            }
        }

        
    }
}
