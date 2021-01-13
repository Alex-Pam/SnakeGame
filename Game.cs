using System;
using System.Threading;
namespace SnakeGame
{
    class Game
    {
        static readonly int x = 80;
        static readonly int y = 26;
        static Walls walls;
        static Food food;
        static Snake snake;
        static Timer time;
        static void Main(string[] args)
        {
            Console.SetWindowSize(Game.x + 1, Game.y + 1);
            Console.SetBufferSize(Game.x + 1, Game.y + 1);
            Console.CursorVisible = false;
            walls = new Walls(x, y, '#');
            food = new Food(x, y, '@');
            food.CreateFood();
            snake = new Snake(x / 2, y/2, 3);
            time = new Timer(Loop, null, 0, 200); 
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.Rotation(key.Key);
                }
            }
            
        }
        static void Loop(object obj)
        {
            if (walls.IsHit(snake.GetHead()) || snake.IsHit(snake.GetHead()))
            {
                time.Change(0, Timeout.Infinite);
            }
            else if (snake.Eat(food.food))
            {
                food.CreateFood();
            }
            else
            {
                snake.Move();
            }
        }
    }
    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char ch { get; set; }
        public static implicit operator Point((int, int, char)value) =>
            new Point {x = value.Item1, y = value.Item2, ch = value.Item3};
        public void Draw() 
        {
            DrawPoint(ch);
        }
        public void Clear()
        {
            DrawPoint(' ');
        }
        public void DrawPoint(char ch_)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(ch_);
        }
        public static bool operator == (Point a, Point b) =>
            (a.x == b.x && a.y == b.y) ? true : false;
        public static bool operator !=(Point a, Point b) =>
            (a.x != b.x && a.y != b.y) ? true : false;
    }
}
