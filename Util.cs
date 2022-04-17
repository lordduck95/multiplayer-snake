using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace sznék_
{
    internal class Util
    {
        public static Point AddPoints(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static List<Rectangle> ConvertPointListToRect(List<Point> points, int width, int height)
        {
            List<Rectangle> outList = new List<Rectangle>();
            foreach (var point in points)
            {
                outList.Add(new Rectangle(point, new Size(width, height)));
            }
            return outList;
        }

        public static void Wait(int milliseconds) //wait util
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        public static (Rectangle rect, Point pt) RandomPos()
        {
            Random rx = new Random();
            Random ry = new Random();
            int x, y;

            x = rx.Next(0, Game.gameWidth / 16);    //thank god for integers rounding down
            y = ry.Next(0, Game.gameHeight / 16);



            return (new Rectangle(16 * x, 16 * y, 16, 16), new Point(16 * x, 16 * y));
        }

    }
}