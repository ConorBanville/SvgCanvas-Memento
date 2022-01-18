using System;

namespace SvgCanvas
{
    public class Rectangle : IShape
    {
        public override string Name {get; set;} // Store the name of the shape, Rectangle
        public override ushort Id {get;} // 16 bit unsigned integer to store id of shape, [0 -> 65535)
        public float X {get; set;} // Store directional X position of the shape
        public float Y {get; set;} // Store directional Y position of the shape
        public float W {get; set;} // Store width of the shape
        public float H {get; set;} // Store height of the shape
        public string[] Styles {get; set;} // Store the style attributes of the shape
        public static string[] DefaultStyles {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape

        // Default constructor
        public Rectangle(ushort id)
        {
           Name = "Rectangle";
           Id = id;
           X = Y = W = H = 0.0F;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions of the shape
        public Rectangle(ushort id, float x, float y, float w, float h)
        {
           Name = "Rectangle";
           Id = id;
           X = x;
           Y = y;
           W = w;
           H = h;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions and the styles of the shape
        public Rectangle(ushort id, float x, float y, float w, float h, string[] styles)
        {
            Name = "Rectangle";
            Id = id;
            X = x;
            Y = y;
            W = w;
            H = h;
            Styles = styles;
        }

        // Setter: Set the values for the Styles array
        public override void SetStyles(string[] styles)
        {
            if(styles.Length != 4) return;
            Styles = styles;
        }

        // Setter: Set the static class member values for DefaultStyles
        public static void SetDefaults(string[] defaultStyles)
        {
            if(defaultStyles.Length != 4) return;
            DefaultStyles = defaultStyles;
        }
    }
}