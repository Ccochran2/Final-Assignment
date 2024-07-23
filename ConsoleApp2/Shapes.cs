using DocumentFormat.OpenXml.Office2010.PowerPoint;
using System.Runtime.CompilerServices;

namespace Shapes
{
    public abstract class Shape
    {
        public abstract double Area();
        public abstract double Perimeter();
        public abstract double Volume();

    }
    public class Triangle : Shape
    {
        /* Represents a 2D triangle in any form.

            For each of the parameters, we setup a private value and a public accessor.

        This way, we can filter out any attempts to set the values to non-positive numbers.
        */
        public double sideA
        {
            get { return sideA; }
            set { if (value > 0) { sideA = value; } }
        }
        public double sideB
        {
            get { return sideB; }
            set { if (value > 0) { sideB = value; } }
        }
        public double sideC
        {
            get { return sideC; }
            set { if (value > 0) { sideC = value; } }
        }

        public Triangle(double sideA, double sideB, double sideC)
        {
            // Checking for positive, non-zero numbers
            string errorMessage = "Every side of the triangle must be a positive, non-negative number. Received a `{0}`";
            if (sideA! > 0) { throw new ArgumentException(string.Format(errorMessage, sideA.ToString())); }
            if (sideB! > 0) { throw new ArgumentException(string.Format(errorMessage, sideB.ToString())); }
            if (sideC! > 0) { throw new ArgumentException(string.Format(errorMessage, sideC.ToString())); }

            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
        }

        public override double Area()
        {
            /* Returns the area of any triangle given we know the three sides.

            We'll be using "Heron's formula" https://en.wikipedia.org/wiki/Triangle#Heron's_formula

            First step is to calculate the 'semiperimeter' or half the triangles perimeter. We can use our perimeter method and half that.

            Then we plug it into this function where s is our semiperimeter
            sqrt{s(s-a)(s-b)(s-c)}
            */
            double semiPerimeter = this.Perimeter() / 2;
            // Returning our completed math function result
            return Math.Sqrt(
              semiPerimeter
              * (semiPerimeter - this.sideA)
              * (semiPerimeter - this.sideB)
              * (semiPerimeter - this.sideC)
            );
        }

        public override double Perimeter()
        {
            return this.sideA
              + this.sideB
              + this.sideC;
        }
        public override double Volume()
        {
            throw new NotImplementedException();
        }
    }

    public class Polygon : Shape
    {
        /* Represents a 2D regular polygon.

        https://en.wikipedia.org/wiki/Regular_polygon

        We're measuring our polygon by the length of it's faces instead of apothem for simplicity.

        faceLength and faces are read-only to prevent manipulation
        */
        private double _faceLength;
        public double faceLength
        {
            get { return _faceLength; }
            set { if (value > 0) { _faceLength = value; } }
        }
        private double _faces;
        public double faces
        {
            get { return _faces; }
            set { if (value >= 3) { _faces = value; } }
        }



        public Polygon(double sideLength, int faces)
        {
            // Checking for invalid arguments
            if (sideLength <= 0)
            {
                throw new ArgumentException($"Side length must be a positive, non-zero number. Received `{sideLength}`");
            }
            if (faces < 3)
            {
                throw new ArgumentException($"The regular polygon must have at least 3 faces. Received `{faces}`");
            }
            // Setting member variables
            this._faceLength = sideLength;
            this._faces = faces;
        }

        public override double Area()
        {
            /* Returns the area of our polygon

            https://en.wikipedia.org/wiki/Regular_polygon#Area

            (n/4) * s^2 * cot(pi/n)

                  where:
                      n: number of sides
                      s: side length
            */
            // There is no 'cot' in our math library
            // Cot can be represented as cos/sin, so we'll do that.
            double cosComponent = Math.Cos(Math.PI / this._faces);
            double sinComponent = Math.Sin(Math.PI / this._faces);
            double cotComponent = cosComponent / sinComponent;

            // The rest of the formula is simple enough for a single line
            return (this._faces / 4) * Math.Pow(this._faceLength, 2) * cotComponent;
        }

        public override double Perimeter()
        {
            // Gives the perimeter of the polygon.
            return this._faceLength * this._faces;
        }
        public override double Volume()
        {
            throw new NotImplementedException();
        }
    }
    public class Cuboid : Shape
    {
        private double height;
        public double Height
        {
            get { return height; }
            set { if (height > 0) { height = value; } }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set { if (width > 0) { width = value; } }
        }
        private double depth;
        public double Depth
        {
            get { return depth; }
            set { if (depth > 0) { depth = value; } }
        }

        public Cuboid(double width, double height, double depth)
        {

            string errorMessage = "Every side of the cuboid must be a positive, non-negative number. Received a `{0}";
            if (height <= 0) { throw new ArgumentException(string.Format(errorMessage, height.ToString())); }
            if (width <= 0) { throw new ArgumentException(string.Format(errorMessage, width.ToString())); }
            if (depth <= 0) { throw new ArgumentException(string.Format(errorMessage, depth.ToString())); }

            this.height = height;
            this.width = width;
            this.depth = depth;
        }

        public override double Area()
        {
            return (2 * (this.depth * this.width + this.width * this.height + this.height * this.depth));
        }

        public override double Volume()
        {
            throw new NotImplementedException();
        }
        public override double Perimeter()
        {
            throw new NotImplementedException();
        }
    }

    public class Cube : Shape
    {
        private double length;
        public double Length
        {
            get { return length; }
            set { if (length > 0) { length = value; } }
        }

        public Cube(double length)
        {
            string errorMessage = $"Every side of the cube must be a positive, non-negative number. Received a `{length}";
            if (length <= 0) { throw new ArgumentException(string.Format(errorMessage, length.ToString())); }

            this.length = length;
        }
        public override double Area()
        {
            return Math.Pow(this.length, 2) * 6;
        }
        public override double Volume()
        {
            throw new NotImplementedException();
        }

        public override double Perimeter()
        {
            throw new NotImplementedException();
        }

    }

    public class Prism : Shape
    {
        private double _faceLength;
        public double faceLength
        {
            get { return _faceLength; }
            set { if (value > 0) { _faceLength = value; } }
        }
        private double _faces;
        public double faces
        {
            get { return _faces; }
            set { if (value >= 3) { _faces = value; } }
        }
        private double _height;
        public double height
        {
            get { return _height; }
            set
            {
                if (value > 0) { height = value; }
            }
        }
        private double surfaceArea = 0;
        private double prismVolume = 0;
        public Prism(double faceLength, double faces, double height)
        {
            if (faceLength <= 0)
            {
                throw new ArgumentException($"Side length must be a positive, non-zero number. Received `{faceLength}`");
            }
            if (faces < 3)
            {
                throw new ArgumentException($"The regular polygon must have at least 3 faces. Received `{faces}`");
            }
            if (height <= 0)
            {
                throw new ArgumentException($"A 3D prism's height must be greater than 0. Received `{height}`");
            }

            this._faceLength = faceLength;
            this._faces = faces;
            this._height = height;

            double cosComponent = Math.Cos(Math.PI / this._faces);
            double sinComponent = Math.Sin(Math.PI / this._faces);
            double cotComponent = cosComponent / sinComponent;

            // The rest of the formula is simple enough for a single line
            prismVolume = (this.faces / 4) * height * Math.Pow(this.faceLength, 2) * cotComponent;
            surfaceArea = (this.faces / 2) * Math.Pow(this.faceLength, 2) * cotComponent + this.faces * this.faceLength * this.height;
        }

        public override double Area()
        {
            return this.surfaceArea;
        }

        public override double Perimeter()
        {
            throw new NotImplementedException();
        }

        public override double Volume()
        {
            return this.prismVolume;
        }
    }
    public class Cylinder : Shape
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
            set { if (radius > value) { radius = value; } }
        }
        private double height;
        public double Height
        {
            get { return height; }
            set { if (height > value) { height = value; } }
        }

        private double cylinderVolume = 0;
        private double cylinderArea = 0;
        public Cylinder(double radius, double height)
        {
            if (radius <= 0)
            {
                throw new ArgumentException($"Radius must be greater than 0 or else this is a single molecule thin cylinder. Received `{radius}`");
            }
            if (height <= 0)
            {
                throw new ArgumentException($"Height must be non-negative, and it must be greater than 0 or else it is a flat circle, not a cylinder. Received `{height}`");
            }

            this.radius = radius;
            this.height = height;

            cylinderVolume = Math.PI * Math.Pow(this.radius, 2) * this.height;
            cylinderArea = 2 * Math.PI * this.radius * this.height + 2 * Math.PI * Math.Pow(this.radius, 2);
        }

        public override double Area()
        {
            return cylinderArea;
        }
        public override double Perimeter()
        {
            throw new NotImplementedException();
        }
        public override double Volume()
        {
            return cylinderVolume;
        }
    }

    public class Sphere : Shape
    {
        private double radius;
        public double Radius { get { return radius; } set { if (radius > 0) { radius = value; } } }

        private double sphereVolume = 0;
        private double sphereArea = 0;
        public Sphere(double radius)
        {
            if (radius <= 0)
            {
                throw new ArgumentException($"Radius must be positive and greater than 0. Received `{radius}`");
            }

            this.radius = radius;
            sphereVolume = (4 / 3) * Math.PI * Math.Pow(radius, 3);
            sphereArea = 4 * Math.PI * Math.Pow(radius, 2);
        }

        public override double Area()
        {
            return sphereArea;
        }
        public override double Perimeter()
        {
            throw new NotImplementedException();
        }
        public override double Volume()
        {
            return sphereVolume;
        }
    }
}