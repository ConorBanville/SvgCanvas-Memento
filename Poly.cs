using System.Collections.Generic;

namespace SvgCanvas
{
    public class Poly : IShape
    {
        public override string Name {get; set;} // Store the name of the shape, Polyline / Polygon
        public override ushort Id {get;} // 16 bit unsigned integer to store id of shape, [0 -> 65535)        
        public List<Point> Points {get; set;} // Store a list of points for the shape
        public string[] Styles {get; set;} // Store the style attributes of the shape
        public static string[] DefaultStylesPolyline {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape
        public static string[] DefaultStylesPolygon {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape

        // Default constructor
        public Poly(ushort id, string name)
        {
           Name = name;
           Id = id;
           Points = new List<Point>();
           if(name == "Polyline")
           {
               Styles = DefaultStylesPolyline;
           }
           else
           {
               Styles = DefaultStylesPolygon;
           }
        }

        // Constructor: Set the dimensions of the shape
        public Poly(ushort id, string name, List<Point> points)
        {
           Name = name;
           Id = id;
           Points = points;
           if(name == "Polyline")
           {
               Styles = DefaultStylesPolyline;
           }
           else
           {
               Styles = DefaultStylesPolygon;
           }
        }

        // Constructor: Set the dimensions and the styles of the shape
        public Poly(ushort id, string name, List<Point> points, string[] styles)
        {
           Name = name;
           Id = id;
           Points = points;
           Styles = styles;
        }

        // Setter: Set the values for the Styles array
        public override void SetStyles(string[] styles)
        {
            if(styles.Length != 4) return;
            Styles = styles;
        }

        // Setter: Set the static class member values for DefaultStyles
        public static void SetDefaults(string[] defaultStyles, bool polyline)
        {
            if(defaultStyles.Length != 4) return;
            
            if(polyline) DefaultStylesPolyline = defaultStyles;
            else DefaultStylesPolygon = defaultStyles;
        }
    }
}