using System;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Media;
namespace ConsoleBomberMan
{
    public struct Board
    {
        Conditions[,] map;
        public int FrameLocationX;
        public int FrameLocationY;
        public int FrameWidthX;
        public int FrameHeightY;
        public int FieldLocationX;
        public int FieldLocationY;
        public int FieldWidthX;
        public int FieldHeightY;
        public int ScoreLivesX;
        public int ScoreLivesY;
        public int ScoreBombsX;
        public int ScoreBombsY;
        public int ScoreFireX;
        public int ScoreFireY;
        public double timeKeyBoard;
        public double timeMoveMob;
        public double timeFire;
        public double timeBombs;
        public double timeGameOver;
    }

    class Program
    {
        public static bool drawGraphics { get; set; } = false;
        static bool gameOverQ = false;
        static Random r = new Random();

        static void Main(string[] args)
        {
            Board board = new Board();

            //Timer timer = new Timer();
            //timer.Elapsed += TimerEWQ;
            //timer.Interval = 10000;
            //timer.Enabled = true;

            //Задание размеров board
            ///Размеры
            board.FrameLocationX = 45;
            board.FrameLocationY = 5;
            //board.FrameLocationX = 0;
            //board.FrameLocationY = 0;
            //board.FrameWidthX = 2+19+2;
            //board.FrameHeightY = 4+11+2;
            board.FieldLocationX = 2;
            board.FieldLocationY = 4;
            //board.FieldWidthX = 20;
            board.FieldWidthX = r.Next(16, 21);
            //board.FieldHeightY = 11;
            board.FieldHeightY = r.Next(8, 12);
            board.ScoreLivesX = 2;
            board.ScoreLivesY = 2;
            board.ScoreBombsX = 8;
            board.ScoreBombsY = 2;
            board.ScoreFireX = 18;
            board.ScoreFireY = 2;
            board.FrameWidthX = 2 + board.FieldWidthX + 1;
            board.FrameHeightY = 4 + board.FieldHeightY + 2;
            board.timeKeyBoard = 1;
            board.timeMoveMob = 125;
            board.timeFire = 2000;
            board.timeBombs = 1000;
            board.timeGameOver = 300;

            DateTime timeKeyBoard = Lib.SetTimeToChange(board.timeKeyBoard);
            DateTime timeMoveMob = Lib.SetTimeToChange(board.timeMoveMob);
            DateTime timeFire = Lib.SetTimeToChange(board.timeFire);
            DateTime timeBombs = Lib.SetTimeToChange(board.timeBombs);
            DateTime timeGameOver = Lib.SetTimeToChange(board.timeGameOver);
            int gameOverX = 0;
            int stepX = 1;

            //Timer gameOverTimer = new Timer();
            //gameOverTimer.Enabled = true;
            //gameOverTimer.Interval = 1000;



            Field field = new Field(board);


            //SoundPlayer music = new SoundPlayer();

            //int loop = 0;

            ConsoleKeyInfo read = new ConsoleKeyInfo();

            do
            {
                //loop++;
                //loop = loop >= 100 ? loop = 0 : loop++;
                if (field.GameOver())//&& (loop >= 100))
                {
                    //gameOverTimer(board, ref timeGameOver);
                    if (Lib.TimeDelay(timeGameOver))
                    {
                        gameOverTimer1(board, gameOverX, ref stepX);
                        gameOverX += stepX;
                        timeGameOver = Lib.SetTimeToChange(board.timeGameOver);
                    }



                    //gameOverTimer.Elapsed += gameOverTimedEvent;
                    //Console.SetCursorPosition(3, 0);
                    //Console.WriteLine("ТЫ ПРОИГРАЛ!!!! АХАХАХАХАХАХАХАХ! D:");
                    //Console.WriteLine("ХОЧЕШЬ НАЧАТЬ НОВУЮ ИГРУ?");
                    //Console.WriteLine("НАЖМИ Y,ЕСЛИ Да, или N, ЕСЛИ Нет");
                    //string k = Console.ReadLine();
                    //if (k.Equals("Y") || k.Equals("y") || k.Equals("н") || k.Equals("Н"))
                    //{
                    //    ///добавить удаление мобов после перезапуска
                    //    Console.Clear();
                    //    field = new Field(board);
                    //}

                }

                //if (Lib.TimeDelay(timeKeyBoard))
                //{
                //    timeKeyBoard = Lib.SetTimeToChange(board.timeKeyBoard);
                if (Console.KeyAvailable && !field.GameOver()) //Опрос нажатой клавиши для движения player
                {
                    read = Console.ReadKey(true);
                    field.ChangePosition(read);
                }

                if (Lib.TimeDelay(timeMoveMob)) //передвижение и отрисовка мобов по времени
                {
                    field.MoveAndDraw();
                    timeMoveMob = Lib.SetTimeToChange(board.timeMoveMob);
                }
                if (Lib.TimeDelay(timeFire)) //Убирание огня с map
                {
                    field.ClearFire();
                    timeFire = Lib.SetTimeToChange(board.timeFire);
                }
                //if (Lib.TimeDelay(timeBombs))
                //{
                //    field.player.bombPub.BombTimer();
                //    timeBombs = Lib.SetTimeToChange(board.timeBombs);
                //}

                //}

            } while (read.Key != ConsoleKey.Escape);

        }

        private static void gameOverTimer(Board board, ref DateTime timeGameOver)
        {
            //if (Lib.TimeDelay(timeGameOver))
            //{

            //    timeGameOver = Lib.SetTimeToChange(board.timeGameOver);
            //}

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            if (Program.drawGraphics == false)
            {
                Program.drawGraphics = true;
                //for (int i = 0; i < 30; i++)
                //{
                //Console.SetCursorPosition(board.FrameLocationX + board.FieldWidthX/2 , board.FieldLocationY + 5);
                Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2);
                //}

                Console.Write("    ");
                Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2 + 1);
                Console.Write("    ");

                string str1 = "GAME";
                string str2 = "OVER";
                if (!gameOverQ)
                {
                    //Console.Write(str1 + "\n" + str2);
                    Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2);
                    Console.Write(str1);
                    Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2 + 1);
                    Console.Write(str2);
                    if (Lib.TimeDelay(timeGameOver))
                    {
                        gameOverQ = true;
                        timeGameOver = Lib.SetTimeToChange(board.timeGameOver);
                    }
                }
                else
                {
                    Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2);
                    Console.Write(str2);
                    Console.SetCursorPosition(board.FrameLocationX + board.FrameWidthX / 2 - 1, board.FrameLocationY + board.FrameHeightY / 2 + 1);
                    Console.Write(str1);
                    if (Lib.TimeDelay(timeGameOver))
                    {
                        gameOverQ = false;
                        timeGameOver = Lib.SetTimeToChange(board.timeGameOver);
                    }
                }
                Program.drawGraphics = false;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        private static void gameOverTimer1(Board board, int gameOverX, ref int stepX)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < board.FrameWidthX + 1; i++)
            {
                Console.SetCursorPosition(board.FrameLocationX + i, board.FrameLocationY);
                Console.Write(" ");
            }
            string gameOver = "GAME OVER";

            gameOverX = gameOverX + stepX;

            if (gameOverX + gameOver.Length - 1 >= board.FrameWidthX)
            {
                //Console.SetCursorPosition(board.FrameLocationX + gameOverX, board.FrameLocationY);
                //Console.Write(gameOver);
                stepX = -1;
            }

            else if(gameOverX <= 0)
            {
                //Console.SetCursorPosition(board.FrameLocationX + gameOverX, board.FrameLocationY);
                //Console.Write(gameOver);
                stepX = 1;
            }
            Console.SetCursorPosition(board.FrameLocationX + gameOverX, board.FrameLocationY);
            Console.Write(gameOver);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;

        }

        
    } 
}
