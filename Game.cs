using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace sznék_
{
    public class Snake
    {
        #region Fields

        public Rectangle collisionBox;

        #endregion Fields

        #region Constructors

        public Snake(Point position, int length)
        {
            Pos = position;
            Length = length;
            SizeOfPieces = 16;
            collisionBox = new Rectangle(new Point(Pos.X + SizeOfPieces, Pos.Y), new Size(SizeOfPieces, SizeOfPieces));
        }

        #endregion Constructors

        #region Properties

        public int Length { get; set; }
        public Point Pos { get; set; }
        public int SizeOfPieces { get; set; }

        #endregion Properties
    }

    internal static class Game
    {
        #region Fields

        public static Rectangle apple;
        public static int gameWidth = Form1.ActiveForm.Width, gameHeight = Form1.ActiveForm.Height - 64;
        public static Snake s = new Snake(new Point(64, 64), 3);
        public static int score = 0;
        public static List<Point> snake = new List<Point>();
        private static string direction = "right";
        private static bool isDead = true;

        #endregion Fields

        #region Methods

        

        public static void Die(Snake sn)
        {
            snake.RemoveRange(0, Game.snake.Count);
            sn.Pos = Util.RandomPos().pt;
            sn.Length = 5;
            score = 0;
            direction = sn.Pos.X <= gameWidth / 2 ? "right" : "left";
            isDead = false;
            apple = Util.RandomPos().rect;
        }

        public static void Go(Snake s, string dir, int speed)
        {
            Point pos;

            switch (dir)
            {
                case "left":
                    //add to position
                    pos = s.Pos;
                    pos.X -= speed;
                    s.Pos = pos;

                    s.collisionBox = new Rectangle(s.Pos, new Size(s.SizeOfPieces, s.SizeOfPieces));
                    break;

                case "right":
                    pos = s.Pos;
                    pos.X += speed;
                    s.Pos = pos;

                    s.collisionBox = new Rectangle(s.Pos, new Size(s.SizeOfPieces, s.SizeOfPieces));
                    break;

                case "up":
                    pos = s.Pos;
                    pos.Y -= speed;
                    s.Pos = pos;

                    s.collisionBox = new Rectangle(s.Pos, new Size(s.SizeOfPieces, s.SizeOfPieces));
                    break;

                case "down":
                    pos = s.Pos;
                    pos.Y += speed;
                    s.Pos = pos;

                    s.collisionBox = new Rectangle(s.Pos, new Size(s.SizeOfPieces, s.SizeOfPieces));
                    break;

                default:
                    break;
            }
            snake.Add(s.Pos);
            if (snake.Count > s.Length)
                snake.RemoveAt(0);
        }

        public static void HandleKeys(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (direction != "down")  //can't turn around
                    {
                        direction = "up";
                    }
                    break;

                case Keys.Down:
                    if (direction != "up")
                    {
                        direction = "down";
                    }
                    break;

                case Keys.Left:
                    if (direction != "right")
                    {
                        direction = "left";
                    }
                    break;

                case Keys.Right:
                    if (direction != "left")
                    {
                        direction = "right";
                    }
                    break;
            }
        }

        public static void UpdateGame(PaintEventArgs e) //when update
        {
            Graphics g = e.Graphics;

            Go(s, direction, 16);

            foreach (var piece in snake.ToList())
            {
                if (!isDead)
                {
                    isDead = CollisionCheck(s, s.collisionBox);
                    if (s.Pos.X <= 0 || s.Pos.Y <= 0 || s.Pos.X + 16 >= gameWidth || s.Pos.Y + 16 >= gameHeight)
                        Die(s);
                    if (CollisionCheck(s, apple))
                    {
                        s.Length++;
                        score++;
                        g.FillRectangle(new SolidBrush(Color.White), apple);
                        apple = Util.RandomPos().rect;
                    }

                    g.FillRectangle(new SolidBrush(Color.LimeGreen), new Rectangle(piece, new Size(s.SizeOfPieces - 1, s.SizeOfPieces - 1)));
                    g.FillRectangle(new SolidBrush(Color.Red), apple);
                }
                else
                {
                    Die(s);
                }
            }


            /*e.Graphics.DrawLine(new Pen(Color.Red), new Point(s.Pos.X, 0), new Point(s.Pos.X, gameHeight));
            e.Graphics.DrawLine(new Pen(Color.Red), new Point(0, s.Pos.Y), new Point(gameWidth, s.Pos.Y));
            e.Graphics.DrawRectangle(new Pen(Color.Red), s.collisionBox);*/
            g.Dispose();


              
        }

        private static bool CollisionCheck(Snake s, Rectangle collider)
        {
            if (!isDead && snake.Count > 0)
            {
                List<Rectangle> snakeInRect = Util.ConvertPointListToRect(snake, s.SizeOfPieces, s.SizeOfPieces).ToList();
                snakeInRect.RemoveAt(snakeInRect.Count - 1);

                foreach (var rect in snakeInRect)
                {
                    if (rect.IntersectsWith(collider))
                    {
                        return true;
                    }
                }

                return false;
            }
            return false;
        }

        #endregion Methods
    }
}