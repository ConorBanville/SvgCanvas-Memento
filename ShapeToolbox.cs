using System.Collections.Generic;
using Newtonsoft.Json;

namespace SvgCanvas
{
    public static class ShapeToolbox
    {

        /*
            GetJson(IShape) takes an IShape as it's argument and returns a json
            string representing the shape passed
        */
        // Return the json for a Rectangle
        public static string GetJson(IShape s)
        {
            return JsonConvert.SerializeObject(s, Formatting.Indented);
        }
        // Return the json for a Circle
        // Return the json for a Ellipse
        // Return the json for a Line
        // Return the json for a Poly
        // Return the json for a Path

        /* 
            Embed(IShape) takes an IShape as it's argument and returns a string 
            which represents the svg of the embeded shape.
        */
        // Return svg embeded Rectangle
        public static string Embed(Rectangle s)
        {
            return $"<rect x=\"{s.X}\" y=\"{s.Y}\" width=\"{s.W}\" height=\"{s.H}\" fill=\"{s.Styles[0]}\" stroke=\"{s.Styles[1]}\" stroke-dasharray=\"{s.Styles[2]}\" stroke-width=\"{s.Styles[3]}\"/>";
        }

        // Return svg embeded Circle
        public static string Embed(Circle s)
        {
            return $"<circle cx=\"{s.X}\" cy=\"{s.Y}\" r=\"{s.Radius}\" fill=\"{s.Styles[0]}\" stroke=\"{s.Styles[1]}\" stroke-dasharray=\"{s.Styles[2]}\" stroke-width=\"{s.Styles[3]}\"/>";
        }

        // Return svg embeded Ellipse
        public static string Embed(Ellipse s)
        {
            return $"<ellipse cx=\"{s.X}\" cy=\"{s.Y}\" rx=\"{s.XRadius}\" ry=\"{s.YRadius}\" fill=\"{s.Styles[0]}\" stroke=\"{s.Styles[1]}\" stroke-dasharray=\"{s.Styles[2]}\" stroke-width=\"{s.Styles[3]}\"/>";
        }

        // Return svg embeded Line
        public static string Embed(Line s)
        {
            return $"<line x1=\"{s.X1}\" y1=\"{s.Y1}\" x2=\"{s.X2}\" y2=\"{s.Y2}\" stroke=\"{s.Styles[0]}\" stroke-dasharray=\"{s.Styles[1]}\" stroke-width=\"{s.Styles[2]}\"/>";
        }

        // Return svg embeded Polyline / Polygon
        public static string Embed(Poly s)
        {
            string name = s.Name.ToLower();
            string points = "";

            if(s.Points.Count > 0)
            {
                foreach(Point p in s.Points)
                {
                    points += $"{p.X},{p.Y} ";
                }

                points = points.Substring(0,points.Length - 1);                
            }

            return $"<{name} points=\"{points}\" fill=\"{s.Styles[0]}\" stroke=\"{s.Styles[1]}\" stroke-dasharray=\"{s.Styles[2]}\" stroke-width=\"{s.Styles[3]}\"/>";
        }

        // Return svg embeded Path
        public static string Embed(Path s)
        {
            return $"<path fill=\"{s.Styles[0]}\" stroke=\"{s.Styles[1]}\" stroke-dasharray=\"{s.Styles[2]}\" stroke-width=\"{s.Styles[3]}\" d=\"{s.Instructions}\"/>";
        }
    

        /*  
            Style(IShape, String) takes a Shape as its argument and returns the same 
            shape with updated styles
        */     
        // Return updated Rectangle
        public static Rectangle Style(Rectangle s, string[] styles)
        {
            if(styles.Length != 4) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Rectangle(s.Id, s.X, s.Y, s.W, s.H, styles); // Return the updated shape
        }

        // Return updated Circle
        public static Circle Style(Circle s, string[] styles)
        {
            if(styles.Length != 4) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Circle(s.Id, s.X, s.Y, s.Radius, styles); // Return the updated shape
        }

        // Return updated Ellipse
        public static Ellipse Style(Ellipse s, string[] styles)
        {
            if(styles.Length != 4) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Ellipse(s.Id, s.X, s.Y, s.XRadius, s.YRadius, styles); // Return the updated shape
        }

        // Return updated Line
        public static Line Style(Line s, string[] styles)
        {
            if(styles.Length != 3) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Line(s.Id, s.X1, s.Y1, s.X2, s.Y2, styles); // Return the updated shape
        }

        // Return updated Polyline / Polygon
        public static Poly Style(Poly s, string[] styles)
        {
            if(styles.Length != 4) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Poly(s.Id, s.Name, s.Points, styles); // Return the updated shape
        }

        // Return updated Path
        public static Path Style(Path s, string[] styles)
        {
            if(styles.Length != 4) return s; // Array must have correct amount of elements
            else if(s == null) return null; // s must not be null
            return new Path(s.Id, s.Instructions, styles); // Return the updated shape
        }
    

        /*
            Edit() takes a Shape and that shape's corresponding data values 
            as it's argument and returns an unpdated shape   
        */
        // Return updated Rectangle
        public static Rectangle Edit(Rectangle s, float x, float y, float w, float h)
        {
            if(s == null) return null; // s must not be null
            return new Rectangle(s.Id, x, y, w, h, s.Styles);
        }

        // Return updated Circle
        public static Circle Edit(Circle s, float x, float y, float radius)
        {
            if(s == null) return null; // s must not be null
            return new Circle(s.Id, x, y, radius, s.Styles); // Return the shape
        }

        // Return updated Ellipse
        public static Ellipse Edit(Ellipse s, float x, float y, float xRadius, float yRadius)
        {
            if(s == null) return null; // s must not be null
            return new Ellipse(s.Id, x, y, xRadius, yRadius, s.Styles); // Return the shape
        }

        // Return updated Line
        public static Line Edit(Line s, float x1, float y1, float x2, float y2)
        {
            if(s == null) return null; // s must not be null
            return new Line(s.Id, x1, y1, x2, y2, s.Styles); // Return the shape
        }

        // Return updated Polyline / Polygon
        public static Poly Edit(Poly s, List<Point> points)
        {
            if(s == null) return null; // s must not be null
            return new Poly(s.Id, s.Name, points, s.Styles); // Return the shape
        }

        // Return updated Path
        public static Path Edit(Path s, string instructions)
        {
            if(s == null) return null; // s must not be null
            return new Path(s.Id, instructions, s.Styles); // Return the shape
        }
    }
}