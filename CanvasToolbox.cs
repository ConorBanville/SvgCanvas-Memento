namespace SvgCanvas
{
    static class CanvasToolbox
    {
        // Return a string contaning the data of each shape on the canvas in Json form
        public static string Display(Canvas c)
        {
           // if(c.Shapes.Count == 0) return "";

            string s = "";
            foreach(IShape shape in c.Shapes)
            {
                s += ShapeToolbox.GetJson(shape);
                s += "\n";
            }

            return s;
        }

        // Remove a shape from the canvas
        public static Canvas RemoveShape(int id, Canvas canvas)
        {
            Canvas c = new Canvas(canvas.GetList());
            bool wasFound = false;
            for(int i=0; i<c.Shapes.Count; i++)
            {
                if(id == c.Shapes[i].Id)
                {
                    c.Shapes.RemoveAt(i);
                    wasFound = true;
                }
            }
            if(wasFound)
            {
                return c;
            }
            else return null;
        }

        // Edit the Z-Index of a shape on the canvas
        public static Canvas Shift(int id, int index, Canvas canvas)
        {
            if(index > canvas.Shapes.Count - 1) return new Canvas();

            Canvas c = new Canvas(canvas.GetList());
            bool wasFound = false;
            for(int i=0; i<c.Shapes.Count; i++)
            {
                if(id == c.Shapes[i].Id)
                {
                    IShape s = c.Shapes[i];
                    c.Shapes.RemoveAt(i);
                    c.Shapes.Insert(index, s);
                    wasFound = true;
                }
            }

            if(wasFound)
            {
                return c;
            }
            else return null;
        }

        // Return a string containing the svg for the cavnas
        public static string GetSVG(Canvas c)
        {
            string svg = $"<?xml version=\"1.0\" standalone=\"no\"?>\n" +
                $"<svg width=\"{c.Width}\" height=\"{c.Height}\" version=\"1.1\" xmlns=\"http://www.w3.org/2000/svg\">\n";
            
            if(c.Shapes.Count != 0)
            {
                for(int i=0; i<c.Shapes.Count; i++)
                {
                    switch(c.Shapes[i].Name)
                    {
                        case "Rectangle":
                            svg += ShapeToolbox.Embed((Rectangle) c.Shapes[i]) + "\n";
                        break;

                        case "Circle":
                            svg += ShapeToolbox.Embed((Circle) c.Shapes[i]) + "\n";
                        break;

                        case "Ellipse":
                            svg += ShapeToolbox.Embed((Ellipse) c.Shapes[i]) + "\n";
                        break;

                        case "Line":
                            svg += ShapeToolbox.Embed((Line) c.Shapes[i]) + "\n";
                        break;

                        case "Polyline":
                            svg += ShapeToolbox.Embed((Poly) c.Shapes[i]) + "\n";
                        break;

                        case "Polygon":
                            svg += ShapeToolbox.Embed((Poly) c.Shapes[i]) + "\n";
                        break;

                        case "Path":
                            svg += ShapeToolbox.Embed((Path) c.Shapes[i]) + "\n";
                        break;
                    }
                }
            }
            
            return svg + "</svg>";
        }
        
    }
}