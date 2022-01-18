using System.Collections.Generic;

namespace SvgCanvas
{
    public class Canvas
    {
        public List<IShape> Shapes {get; set;} // Store a list of shapes on the canvas
        public float Width {get; set;} // Store the width of the canvas
        public float Height {get; set;} // Store the height of the canvas

        // Default Constructor
        public Canvas()
        {
            Shapes = new List<IShape>();
            Width = Height = 1000.0F;
        }
        // Constructor: Takes a list of shapes 
        public Canvas(List<IShape> shapes)
        {
            Shapes = shapes;
            Width = Height = 1000.0F;
        }
        // Constructor: Takes a list of shapes, width and height of the canvas
        public Canvas(List<IShape> shapes, float width, float height)
        {
            Shapes = shapes;
            Width = width;
            Height = height;
        }

        public List<IShape> GetList()
        {
            List<IShape> s = new List<IShape>();

            foreach(var obj in Shapes)
            {
                s.Add(obj);
            }

            return s;
        }
    }
}