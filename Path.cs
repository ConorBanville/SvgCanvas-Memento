namespace SvgCanvas
{
    public class Path : IShape
    {
        public override string Name {get; set;} // Store the name of the shape, Path
        public override ushort Id {get;} // 16 bit unsigned integer to store id of shape, [0 -> 65535)   
        public string Instructions {get; set;} // Store a set of instructions for the shape
        public string[] Styles {get; set;} // Store the style attributes of the shape
        public static string[] DefaultStyles {get; set;} = new string[] {"transparent","black","","1"}; // Store the default style attributes of the shape

        // Default constructor
        public Path(ushort id)
        {
            Name = "Path";
            Id = id;
            Instructions = "";
            Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions of the shape
        public Path(ushort id, string instructions)
        {
            Name = "Path";
            Id = id;
            Instructions = instructions;
            Styles = DefaultStyles;
        }

        // Constructor: Set the dimensions and the styles of the shape
        public Path(ushort id, string instructions, string[] styles)
        {
            Name = "Path";
            Id = id;
            Instructions = instructions;
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