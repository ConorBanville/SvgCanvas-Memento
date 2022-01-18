namespace SvgCanvas
{
    public abstract class IShape
    {
        public abstract string Name {get; set;} // Name of the shape eg. "Rectangle"
        public abstract ushort Id {get;}   // ID number of the shape 
        public abstract void SetStyles(string[] styles); // Set the style values for a shape

    }
}
