using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal class Knot
    {
        public int X;
        public int Y;
        public Knot? next;

        public void AddNext(int number)
        {
            next = new()
            {
                X = 0,
                Y = 0
            };
            if (number == 1) return;
            next.AddNext(number -1);
        }

        public (int, int) Position()
        {
            return (X, Y);
        }

        public void Move(int x, int y, List<(int, int)> map)
        {
            for (int i = 0; i < Math.Abs(x + y); i++)
            {
                if (x != 0) X += x / Math.Abs(x);
                if (y != 0) Y += y / Math.Abs(y);
                /*Console.WriteLine("");
                Console.Write(" (" + X + "," + Y + ")");*/
                if (next != null) next.Follow((X, Y), map);
            }
        }

        public void Follow((int x, int y) head, List<(int, int)> map)
        {
            //Console.WriteLine(head.x + " " + head.y);
            if (head.x == X || head.y == Y)
            {
                if (Math.Abs(head.x - X) == 2)
                {
                    X += (head.x - X) / 2;
                }
                else if (Math.Abs(head.y - Y) == 2)
                {
                    Y += (head.y - Y) / 2;
                }
            }
            else if (Math.Sqrt(Math.Abs(head.x - X)) + Math.Sqrt(Math.Abs(head.y - Y)) > 2)
            {
                if (Math.Abs(head.x - X) == 2)
                {
                    X += (head.x - X) / 2;
                    Y += (head.y - Y);
                }
                else if (Math.Abs(head.y - Y) == 2)
                {
                    Y += (head.y - Y) / 2;
                    X += (head.x - X);
                }
            }
            // Console.Write(" (" + X + "," + Y + ")");
            if (next != null) next.Follow((X, Y), map);
            else map.Add((X, Y));
        }
    }

    internal class Rope
    {

        internal static void SimulateKnot()
        {
            var input = File.ReadLines("../../../input9.txt");
            Knot head = new()
            {
                X = 0,
                Y = 0,
            };
            head.AddNext(1);

            int summ = 0;
            List<(int, int)> map = new()
            {
                head.Position()
            };
            foreach (var line in input)
            {
                var text = line.Split(" ");
                summ = int.Parse(text[1]);
                switch (text[0])
                {
                    case "U":
                        head.Move(0, -1 * summ, map);
                        break;
                    case "R":
                        head.Move(summ, 0, map);
                        break;
                    case "D":
                        head.Move(0, summ, map);
                        break;
                    case "L":
                        head.Move(-1 * summ, 0, map);
                        break;
                }
            }
            Console.WriteLine("la corde a parcourue : " + map.Distinct().Count());
        }

        internal static void Simulate()
        {
            var input = File.ReadLines("../../../input9.txt");
            (int x, int y) tail = (0, 0);
            (int x, int y) head = (0, 0);
            int summ = 0;
            List<(int, int)> map = new();
            map.Add(head);
            foreach(var line in input)
            {
                var text = line.Split(" ");
                summ += int.Parse(text[1]);
                switch(text[0])
                {
                    case "U":
                        (head, tail) = Move(head, tail, 0, -1 * int.Parse(text[1]), map);
                        break;
                    case "R":
                        (head, tail) = Move(head, tail, int.Parse(text[1]), 0, map);
                        break;
                    case "D":
                        (head, tail) = Move(head, tail, 0, int.Parse(text[1]), map);
                        break;
                    case "L":
                        (head, tail) = Move(head, tail, -1 * int.Parse(text[1]), 0, map);
                        break;
                }
            }
            Console.WriteLine("la corde a parcourue : " +  map.Distinct().Count());
        }
        internal static ((int, int), (int, int)) Move((int x, int y) head, (int x, int y)tail, int x, int y, List<(int, int)> map)
        {
            for (int i = 0; i < Math.Abs(x +y); i++)
            {
                if(x != 0) head.x += x / Math.Abs(x);
                if(y != 0) head.y += y / Math.Abs(y);

                //if(!map.Contains(head)) map.Add(head);


                if(head.x == tail.x || head.y == tail.y)
                {
                    if(Math.Abs(head.x - tail.x ) == 2) 
                    {
                        tail.x += (head.x - tail.x)/ 2;
                    }else if (Math.Abs(head.y- tail.y ) == 2)
                    {
                        tail.y += (head.y - tail.y) / 2;
                    }
                }
                else if(Math.Sqrt(Math.Abs(head.x-tail.x))+Math.Sqrt(Math.Abs(head.y-tail.y))> 2)
                {
                    if (Math.Abs(head.x - tail.x) == 2)
                    {
                        tail.x += (head.x - tail.x) / 2;
                        tail.y += (head.y - tail.y);
                    }
                    else if (Math.Abs(head.y - tail.y) == 2)
                    {
                        tail.y += (head.y - tail.y) / 2;
                        tail.x += (head.x - tail.x);
                    }
                }
                if (!map.Contains(tail)) map.Add(tail);
                Console.WriteLine("head :" + head + " tail :" + tail);

            }
            return (head, tail);
        }
    }

}
