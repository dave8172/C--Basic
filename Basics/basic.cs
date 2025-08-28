public class Shape
    {
        private string _color;
        private string _name;

        public string Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Shape(string color, string name)
        {
            _color = color;
            _name = name;
        }

        public virtual void DisplayInfo() // virtual for polymorphism
        {
            Console.WriteLine($"Shape: {Name}, Color: {Color}");
        }
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(string color, string name, double radius) : base(color, name)
        {
            Radius = radius;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Radius: {Radius}");
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(string color, string name, double width, double height) : base(color, name)
        {
            Width = width;
            Height = height;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Width: {Width}, Height: {Height}");
        }
    }

    public class ShapeProgram
    {
        public static void defineShapes()
        {
            List<Shape> shapes = new List<Shape>();

            shapes.Add(new Circle("Red", "My Circle", 5.0));
            shapes.Add(new Rectangle("Blue", "My Rectangle", 10.0, 7.0));

            foreach (Shape shape in shapes)
            {
                shape.DisplayInfo();
                Console.WriteLine();
            }
        }
    }