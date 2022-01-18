namespace SvgCanvas
{
    public class Line : IShape
    {
        public override string Name {get; set;} // Store the name of the shape, Line
        public override ushort Id {get;} // 16 bit unsigned integer to store id of shape, [0 -> 65535)
        public float X1 {get; set;} // Store directional x1 position of the shape
        public float Y1 {get; set;} // Store directional y1 position of the shape
        public float X2 {get; set;} // Store directional x2 position of the shape
        public float Y2 {get; set;} // Store directional y2 position of the shape
        public string[] Styles {get; set;} // Store the style attributes of the shape
        public static string[] DefaultStyles {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape

        // Default constructor
        public Line(ushort id)
        {
           Name = "Line";
           Id = id;
           X1 = Y1 = X2 = Y2 = 0.0F;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions of the shape
        public Line(ushort id, float x1, float y1, float x2, float y2)
        {
           Name = "Line";
           Id = id;
           X1 = x1;
           Y1 = y1;
           X2 = x2;
           Y2 = y2;
           Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions and the styles of the shape
        public Line(ushort id, float x1, float y1, float x2, float y2, string[] styles)
        {
           Name = "Line";
           Id = id;
           X1 = x1;
           Y1 = y1;
           X2 = x2;
           Y2 = y2;
           Styles = styles;
        }

        // Setter: Set the values for the Styles array
        public override void SetStyles(string[] styles)
        {
            if(styles.Length != 3) return;
            Styles = styles;
        }

        // Setter: Set the static class member values for DefaultStyles
        public static void SetDefaults(string[] defaultStyles)
        {
            if(defaultStyles.Length != 3) return;
            DefaultStyles = defaultStyles;
        }
    }
}