namespace SvgCanvas
{
    public class Ellipse : IShape
    {
        public override string Name {get; set;} // Store the name of the shape, Ellipse
        public override ushort Id {get;} // 16 bit unsigned integer to store id of shape, [0 -> 65535)
        public float X {get; set;} // Store directional X position of the shape
        public float Y {get; set;} // Store directional Y position of the shape
        public float XRadius {get; set;} // Store X directional radius of ehe shape
        public float YRadius {get; set;} // Store Y directional radius of the shape
        public string[] Styles {get; set;} // Store the style attributes of the shape
        public static string[] DefaultStyles {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape

        // Default constructor
        public Ellipse(ushort id)
        {
           Name = "Ellipse";
           Id = id;
           X = Y = XRadius = YRadius = 0.0F;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions of the shape
        public Ellipse(ushort id, float x, float y, float xRadius, float yRadius)
        {
           Name = "Ellipse";
           Id = id;
           X = x;
           Y = y;
           XRadius = xRadius;
           YRadius = yRadius;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions and the styles of the shape
        public Ellipse(ushort id, float x, float y, float xRadius, float yRadius, string[] styles)
        {
           Name = "Ellipse";
           Id = id;
           X = x;
           Y = y;
           XRadius = xRadius;
           YRadius = yRadius;
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