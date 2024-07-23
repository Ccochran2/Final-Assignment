using System.IO;
using Shapes;


namespace Solver
{
    static class Solver
    {
        public static void Main(string[] args)
        {
            StreamReader reader = null;
            double totalSum = 0;
            List<Shape> shapeList = new List<Shape>(); // empty list

            foreach (string line in System.IO.File.ReadLines(@"E:\c#\ConsoleApp2\exampleInput.csv"))
            {
                string[] lineData = line.Split(',');

                switch (lineData[0])
                {

                    //Cases for constructing different shapes based on the first thing in any given array of .Split (which should be the name of the shape since that always goes first). Adds them to the empty list for later use.
                    case "triangle":
                        Shape newTriangle = new Triangle(Convert.ToDouble(lineData[1]), Convert.ToDouble(lineData[2]), Convert.ToDouble(lineData[3]));
                        shapeList.Add(newTriangle);
                        break;
                    case "cube":
                        Shape newCube = new Cube(Convert.ToDouble(lineData[1]));
                        shapeList.Add(newCube);
                        break;
                    case "cuboid":
                        Shape newCuboid = new Cuboid(Convert.ToDouble(lineData[1]), Convert.ToDouble(lineData[2]), Convert.ToDouble(lineData[3]));
                        shapeList.Add(newCuboid);
                        break;
                    case "polygon":
                        Shape newPolygon = new Polygon(Convert.ToDouble(lineData[1]), Convert.ToInt16(lineData[2]));
                        shapeList.Add(newPolygon);
                        break;
                    case "sphere":
                        Shape newSphere = new Sphere(Convert.ToDouble(lineData[1]));
                        shapeList.Add(newSphere);
                        break;
                    case "cylinder":
                        Shape newCylinder = new Cylinder(Convert.ToDouble(lineData[1]), Convert.ToDouble(lineData[2]));
                        shapeList.Add(newCylinder);
                        break;
                    case "prism":
                        Shape newPrism = new Prism(Convert.ToDouble(lineData[1]), Convert.ToDouble(lineData[2]), Convert.ToDouble(lineData[3]));
                        shapeList.Add(newPrism);
                        break;
                    
                    //Area Calculations
                    case "area":
                        double areaMultiplier = Convert.ToDouble(lineData[1]); //Change the area multiplier to the value on the line.

                        totalSum = 0; //Reset the total sum so we're not just adding to the previous Total Sum.

                        foreach (var shape in shapeList) //Calculate individual totals and add them to the Total Sum which includes them all + the area/volume multiplier specified.
                        {
                            double areaResult = shape.Area();
                            
                            totalSum = totalSum + (areaResult * areaMultiplier); //Should still calculate accurately with the multiplier. 3 * ABC is the same as 3A * 3B * 3C if I recall my maths correctly but also I'm writing this on 4 hours of sleep after being awake for 22+ hours.

                            Console.Write($"{Environment.NewLine}{totalSum}");
                            
                        }
                        shapeList.Clear(); //Clear for the next set of inputs
                        Console.WriteLine("Emptied list.");
                        break;
                    
                    //Volume Calculations
                    case "volume":
                        double volumeMultiplier = Convert.ToDouble(lineData[1]); //Change the volume multiplier to the value on the line.

                        totalSum = 0; //Reset the total sum so we're not just adding to the previous Total Sum.

                        foreach (var shape in shapeList) //Calculate individual totals and add them to the Total Sum which includes them all + the area/volume multiplier specified.
                        {
                            double volumeResult = shape.Volume();

                            totalSum = totalSum + (volumeResult * volumeMultiplier); //Should still calculate accurately with the multiplier. 3 * ABC is the same as 3A * 3B * 3C if I recall my maths correctly but also I'm writing this on 4 hours of sleep after being awake for 22+ hours.

                            Console.Write($"{Environment.NewLine}{totalSum}");

                        }
                        
                        shapeList.Clear(); //Clear for the next set of inputs
                        break;

                    default:
                        break;
                }
            }
        }
    }
}