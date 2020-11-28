using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{
    abstract class Shape
    {
        public Shape() { }
        public abstract double Area();
        public abstract string Show();
    }

    class Square : Shape
    {
        private double side { get; set; }
        public Square(double _side)
        {
            this.side = _side;
        }

        public override double Area()
        {
            return side * side;
        }

        public override string Show()
        {
            return "Square area: " + this.Area();
        }
    }

    class Triangle : Shape
    {
        private double tbase { get; set; }
        private double height { get; set; }
        public Triangle(double _tbase, double _height)
        {
            this.height = _height;
            this.tbase = _tbase;
        }

        public override double Area()
        {
            return height * tbase / 2;
        }
        public override string Show()
        {
            return "Triangle area: " + this.Area();
        }

    }
    class Circle : Shape
    {
        private double radius { get; set; }
        public Circle(double _radius)
        {
            this.radius = _radius;
        }

        public override double Area()
        {
            return radius * radius * Math.PI;
        }
        public override string Show()
        {
            return "Circle area: " + this.Area();
        }
    }
    class Rectangle : Shape
    {
        private double width { get; set; }
        private double height { get; set; }
        public Rectangle(double _a, double _b)
        {
            this.width = _a;
            this.height = _b;
        }

        public override double Area()
        {
            return width * height;
        }
        public override string Show()
        {
            return "Rectangle area: " + this.Area();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double side = 11.1;
            double radius = 2.1;
            double height = 5.354;
            double width = 13;
            double tbase = 14;
            double theight = 5;

            List<Shape> shapres = new List<Shape> { new Square(side), new Circle(radius), new Rectangle(height, width), new Triangle(tbase, theight) };
            Console.WriteLine("Before sort\n");
            foreach (var item in shapres)
            {
                Console.WriteLine(item.Show());
            }
            shapres.Sort((a, b) => a.Area().CompareTo(b.Area()));
            Console.WriteLine("\n\nAfter sort\n");
            foreach (var item in shapres)
            {
                Console.WriteLine(item.Show());
            }
            Console.ReadKey();
        }
    }
}
