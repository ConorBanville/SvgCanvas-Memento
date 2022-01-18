using System;
using System.IO;
using System.Collections.Generic;

namespace SvgCanvas

/*
    The Manager class is the centre of the application, it handles taking input 
    from the user and then parses and executes that input. It also manages the 
    actors in the Memento DP, ie. Contains the caretaker.
*/

{
    class Manager
    {
        public Caretaker caretaker; // Caretaker to implement Memento DP
        static Memento memento; // Store a memento
        static Canvas C; // Store the canvas
        ushort ShapesId; // Track the Id of each new shape
        float Width, Height; // Store the width and height of the cavnas

        // Define some test commands 

        string[] _testCommands = {
             "rect (20,20,20,20)",
             "circle (10,10,10)",
             "ellipse (100,100,200,200)",
             "line (100,100,700,700)",
             "polyline (0,0,10,10,50,50,70,70,100,100)",
             "polygon (1000,1000,980,980,200,100)",
             "path (M150 0 L75 200 L225 200 Z)",
             "canvas (2000,2000)",
             "export (MyFile)",
             "remove (3)",
             "remove (200)",
             "style (1, Blue, Red, 12, 2)",
             "edit (0,0,0,0,0)",
             "default-style (circle, Blue, Red, 12, 2)",
             "circle (1000,1000,100)",
             "shift (0,6)",
             "shift (1,100)",
         };

        int _indexer = 0;    // track the index of the test commands

        public Manager()
        {
            C = new Canvas();
            caretaker = new Caretaker(new Canvas());
            caretaker.BackUp();
            ShapesId = 0;
            Width = Height = 1000.0F;
        }

        public void Start()
        {
            bool Running = true;
            string Input;

            Console.Clear(); // Clear the screen 
            Console.WriteLine(Screens.Banner);  // Print the Banner
            Console.WriteLine(Screens.Welcome); // Print the welcome message

            while(Running)
            {
                Input = Console.ReadLine();
                switch(Run(Input))
                {
                    case "k":
                        Console.WriteLine("Program Closing ...");
                    return;

                    default:
                    break;
                }
            }
        }

        public string Run(string input)
        {
            string Token;
            string Argument;

            if(input.Contains(" "))
            {
                Token = input.Substring(0,input.IndexOf(' '));
                Argument = input.Substring(input.IndexOf(' ') + 1, input.Length - (input.IndexOf(' ') + 1));
                return Parse(Token, Argument);
            }
            else
            {
                Token = input;
                Argument = "";
                return Parse(Token);
            }
        }

        public string Parse(string token)
        {
            switch(token.ToLower())
            {
                // Step through some predefined test commands
                case "n":
                    Console.WriteLine($"Running test command: {_testCommands[_indexer]}");
                    Run(_testCommands[_indexer]);
                    _indexer++;
                    if (_indexer > _testCommands.Length - 1) _indexer = _testCommands.Length - 1;
                break;

                // Display the help screen
                case "h":
                    Console.WriteLine(Screens.Help);
                break;
                // Display the help screen
                case "help":
                    Console.WriteLine(Screens.Help);
                break;
                // Display the list screen
                case "l":
                    Console.WriteLine(Screens.List);
                break;
                // Display the list screen
                case "list":
                    Console.WriteLine(Screens.List);
                break;
                // Close the Program 
                case "k":
                return "k";
                // Close the Program 
                case "kill":
                return "k";
                // undo last command
                case "u":
                    caretaker.Undo();
                break;
                // undo last command
                case "undo":
                    caretaker.Undo();
                break;
                // redo last command
                case "r":
                    caretaker.Redo();
                break;
                // redo last command
                case "redo":
                    caretaker.Redo();
                break;
                // Display a list of shape currently on the canvas
                case "c":
                    C = caretaker.GetState().canvas;
                    Console.WriteLine($"Width = {C.Width}, Height = {C.Height}");
                    Console.WriteLine(CanvasToolbox.Display(caretaker.GetState().canvas));
                break;
                // Export the canvas using a default name
                case "e":
                    string svg = CanvasToolbox.GetSVG(caretaker.GetState().canvas);
                    string fileName = DateTime.Now.ToString("dddd, dd MM yyyy HH-mm-ss");
                    File.WriteAllText($"./output/{fileName}.svg",svg);
                break;
                // Handle all other input
                default:
                    Console.WriteLine($"Could not understand \"{token}\"");
                break;
            }

            return "";
        }

        // Execute inputs that cantain argument 
        public string Parse(string token, string argument)
        {
            string[] data;

            switch(token.ToLower())
            {
                case "c":
                    argument = argument.Substring(1,argument.Length - 2);
                    data =  argument.Split(',');
                    try
                    {
                        C = new Canvas(caretaker.GetState().canvas.Shapes, float.Parse(data[0]), float.Parse(data[1]));
                        Width = float.Parse(data[0]);
                        Height = float.Parse(data[1]);
                        caretaker.o.NewState(C);
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                break;

                case "canvas":
                    argument = argument.Substring(1,argument.Length - 2);
                    data =  argument.Split(',');
                    try
                    {
                        C = new Canvas(caretaker.GetState().canvas.Shapes, float.Parse(data[0]), float.Parse(data[1]));
                        Width = float.Parse(data[0]);
                        Height = float.Parse(data[1]);
                        caretaker.o.NewState(C);
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                break;

                // Export the svg with a file name specified
                case "e":
                    C = caretaker.GetState().canvas;
                    C.Width = Width;
                    C.Height = Height;
                    string svg = CanvasToolbox.GetSVG(C);
                    string fileName = argument.Substring(1,argument.Length - 2);
                    File.WriteAllText($"./output/{fileName}.svg",svg);
                break;
                // Export the svg with a file name specified
                case "export":
                    C = caretaker.GetState().canvas;
                    C.Width = Width;
                    C.Height = Height;
                    svg = CanvasToolbox.GetSVG(C);
                    fileName = argument.Substring(1,argument.Length - 2);
                    File.WriteAllText($"./output/{fileName}.svg",svg);
                break;

                case "remove":
                    argument = argument.Substring(1,argument.Length - 2);
                    try
                    {
                        int id = Convert.ToInt32(argument);
                        C = CanvasToolbox.RemoveShape(id,caretaker.GetState().canvas);
                        if(C == null)
                        {
                            Console.WriteLine($"Shape with Id = ({id}) was not found on the canvas!");
                        }
                        else
                        {
                            caretaker.o.NewState(C);
                            caretaker.BackUp();
                        }
                    }
                    catch (Exception e)
                    {
                        // Display an error message
                        Console.WriteLine($"ERROR: Failed to parse a valid Id from ({argument})");
                        Console.WriteLine($"Exception: {e.Message}");
                    }
                break;

                case "edit":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    C = new Canvas(caretaker.GetState().canvas.GetList());

                    try
                    {
                        int id = Convert.ToInt32(data[0]);
                        for(int i=0; i<C.Shapes.Count; i++)
                        {
                            if(id == C.Shapes[i].Id)
                            {
                                switch(C.Shapes[i].Name)
                                {
                                    case "Rectangle":
                                        Rectangle r = ShapeToolbox.Edit((Rectangle) C.Shapes[i], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]));
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, r);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Circle":
                                        Circle c = ShapeToolbox.Edit((Circle) C.Shapes[i], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                                        if(c == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, c);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Ellipse":
                                        Ellipse e = ShapeToolbox.Edit((Ellipse) C.Shapes[i], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]));
                                        if(e == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, e);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Line":
                                        Line l = ShapeToolbox.Edit((Line) C.Shapes[i], float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]), float.Parse(data[4]));
                                        if(l == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, l);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Polyline":
                                        List<Point> points = new List<Point>();
                                        if(data.Length % 2 == 0)
                                        {
                                            Console.WriteLine($"ERROR: Failed to edit a Polyline from ({argument})");
                                            Console.WriteLine("Must have and X and Y for each point!");
                                        }
                                        for(int j=1; i<data.Length; i++)
                                        {
                                            points.Add(new Point(float.Parse(data[i]), float.Parse(data[i+1])));
                                            j++;
                                        }
                                        Poly pl = ShapeToolbox.Edit((Poly) C.Shapes[i], points);
                                        if(pl == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pl);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Polygon":
                                        points = new List<Point>();
                                        if(data.Length % 2 == 0)
                                        {
                                            Console.WriteLine($"ERROR: Failed to edit a Polygon from ({argument})");
                                            Console.WriteLine("Must have and X and Y for each point!");
                                        }
                                        for(int j=1; i<data.Length; i++)
                                        {
                                            points.Add(new Point(float.Parse(data[i]), float.Parse(data[i+1])));
                                            j++;
                                        }
                                        Poly pg = ShapeToolbox.Edit((Poly) C.Shapes[i], points);
                                        if(pg == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pg);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Path":
                                        Path pth = ShapeToolbox.Edit((Path) C.Shapes[i], argument);
                                        if(pth == null) break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pth);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception e) 
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                break;

                case "style":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');

                    C = new Canvas(caretaker.GetState().canvas.GetList());

                    try
                    {
                        int id = Convert.ToInt32(data[0]); 
                        for(int i=0; i<C.Shapes.Count; i++)
                        {
                            if(id == C.Shapes[i].Id)
                            {
                                switch(C.Shapes[i].Name)
                                {
                                    case "Rectangle":
                                        Rectangle r = ShapeToolbox.Style((Rectangle) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, r);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Circle":
                                        Circle c = ShapeToolbox.Style((Circle) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        if(c == null)break;
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, c);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Line":
                                        Line l = ShapeToolbox.Style((Line) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, l);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Ellipse":
                                        Ellipse e = ShapeToolbox.Style((Ellipse) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, e);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Polyline":
                                        Poly pl = ShapeToolbox.Style((Poly) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pl);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Polygon":
                                        Poly pg = ShapeToolbox.Style((Poly) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pg);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;

                                    case "Path":
                                        Path pth = ShapeToolbox.Style((Path) C.Shapes[i],new string[]{data[1],data[2],data[3],data[4]});
                                        C.Shapes.RemoveAt(i);
                                        C.Shapes.Insert(i, pth);
                                        caretaker.o.NewState(C);
                                        caretaker.BackUp();
                                    break;
                                }
                            }
                        }

                        caretaker.o.NewState(C);
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                    
                break;

                case "default-style":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        switch(data[0].ToLower())
                        {
                            case "rect":
                                Rectangle.SetDefaults(new string[]{data[1],data[2],data[3],data[4]});
                            break;

                            case "circle":
                                Circle.SetDefaults(new string[]{data[1],data[2],data[3],data[4]});
                            break;

                            case "ellipse":
                                Ellipse.SetDefaults(new string[]{data[1],data[2],data[3],data[4]});
                            break;

                            case "line":
                                Line.SetDefaults(new string[]{data[1],data[2],data[3]});
                            break;

                            case "polyline":
                                Poly.SetDefaults(new string[]{data[1],data[2],data[3],data[4]}, true);
                            break;

                            case "polygon":
                                Poly.SetDefaults(new string[]{data[1],data[2],data[3],data[4]}, false);
                            break;

                            case "path":
                                Path.SetDefaults(new string[]{data[1],data[2],data[3],data[4]});
                            break;

                            default:
                                Console.WriteLine($"{data[0]} is not a valid shape name.");
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                break;

                case "shift":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        C = new Canvas(caretaker.GetState().canvas.GetList());
                        C = CanvasToolbox.Shift(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), C);
                        if(C == null)
                        {
                            Console.WriteLine($"Shape with Id = ({data[0]}) was not found on the canvas!");
                        }
                        else if(C.Shapes.Count == 0)
                        {
                            Console.WriteLine("ERROR: Index is out of bounds!");
                            Console.WriteLine($"OUT OF BOUNDS EXCEPTION: {data[1]} > {caretaker.GetState().canvas.Shapes.Count - 1}");
                        }
                        else
                        {
                            caretaker.o.NewState(C);
                            caretaker.BackUp();
                        }
                    }
                    catch (Exception e) 
                    {
                        Console.WriteLine($"ERROR: failed to parse {token} ({argument})");
                        Console.WriteLine(e.Message);
                    }
                break;

                // Add a recangle to the canvas
                case "rect":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        // Create a new shape from the data
                        Rectangle r = new Rectangle(ShapesId, float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                        // Increment the shape id counter
                        ShapesId++;
                        // Get the current state
                        memento = caretaker.GetState();
                        // Create a new canvas equal to the canvas in the current state
                        C = new Canvas(memento.canvas.GetList(),C.Width,C.Height);
                        // Add the new shape to that canvas
                        C.Shapes.Add(r);
                        // Add the new state to the caretaker
                        caretaker.o.NewState(C);
                        // Backup the new state to the history
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        // Display an error message
                        Console.WriteLine($"ERROR: Failed to create a Rectangle from ({argument})");
                        Console.WriteLine($"Exception: {e.Message}");
                    }
                break;

                // Add a circle to the canvas
                case "circle":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        // Create a new shape from the data
                        Circle c = new Circle(ShapesId, float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
                        // Increment the shape id counter
                        ShapesId++;
                        // Get the current state
                        memento = caretaker.GetState();
                        // Create a new canvas equal to the canvas in the current state
                        C = new Canvas(memento.canvas.GetList(),C.Width,C.Height);
                        // Add the new shape to that canvas
                        C.Shapes.Add(c);
                        // Add the new state to the caretaker
                        caretaker.o.NewState(C);
                        // Backup the new state to the history
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        // Display an error message
                        Console.WriteLine($"ERROR: Failed to create a Circle from ({argument})");
                        Console.WriteLine($"Exception: {e.Message}");
                    }
                break;

                // Add an ellipse to the canvas
                case "ellipse":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        // Create a new shape from the data
                        Ellipse c = new Ellipse(ShapesId, float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                        // Increment the shape id counter
                        ShapesId++;
                        // Get the current state
                        memento = caretaker.GetState();
                        // Create a new canvas equal to the canvas in the current state
                        C = new Canvas(memento.canvas.GetList(),C.Width,C.Height);
                        // Add the new shape to that canvas
                        C.Shapes.Add(c);
                        // Add the new state to the caretaker
                        caretaker.o.NewState(C);
                        // Backup the new state to the history
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        // Display an error message
                        Console.WriteLine($"ERROR: Failed to create a Ellipse from ({argument})");
                        Console.WriteLine($"Exception: {e.Message}");
                    }
                break;

                case "line":
                    argument = argument.Substring(1,argument.Length - 2);
                    data = argument.Split(',');
                    try
                    {
                        // Create a new shape from the data
                        Line c = new Line(ShapesId, float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
                        // Increment the shape id counter
                        ShapesId++;
                        // Get the current state
                        memento = caretaker.GetState();
                        // Create a new canvas equal to the canvas in the current state
                        C = new Canvas(memento.canvas.GetList(),C.Width,C.Height);
                        // Add the new shape to that canvas
                        C.Shapes.Add(c);
                        // Add the new state to the caretaker
                        caretaker.o.NewState(C);
                        // Backup the new state to the history
                        caretaker.BackUp();
                    }
                    catch (Exception e)
                    {
                        // Display an error message
                        Console.WriteLine($"ERROR: Failed to create a Line from ({argument})");
                        Console.WriteLine($"Exception: {e.Message}");
                    }
                break;

                case "polyline":
                    argument = argument.Substring( 1, argument.Length - 2);
                    data = argument.Split(',');

                    if(data.Length % 2 == 0)
                    {
                        try
                        {
                            List<Point> points = new List<Point>();
                            for(int i=0; i<data.Length; i++)
                            {
                                Point p = new Point(float.Parse(data[i]), float.Parse(data[i+1]));
                                points.Add(p);
                                i++;
                            }
                            Poly polyline = new Poly(ShapesId,"Polyline",points);
                            ShapesId ++;
                            C = new Canvas(caretaker.GetState().canvas.GetList());
                            C.Shapes.Add(polyline);
                            caretaker.o.NewState(C);
                            caretaker.BackUp();
                        }
                        catch (Exception e)
                        {
                            // Display an error message
                            Console.WriteLine($"ERROR: Failed to create a Polyline from ({argument})");
                            Console.WriteLine($"Exception: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Failed to create a Polyline from ({argument})");
                        Console.WriteLine("Must have and X and Y for each point!");
                    }
                break;

                case "polygon":
                    argument = argument.Substring( 1, argument.Length - 2);
                    data = argument.Split(',');

                    if(data.Length % 2 == 0)
                    {
                        try
                        {
                            List<Point> points = new List<Point>();
                            for(int i=0; i<data.Length; i++)
                            {
                                Point p = new Point(float.Parse(data[i]), float.Parse(data[i+1]));
                                points.Add(p);
                                i++;
                            }
                            Poly polyline = new Poly(ShapesId,"Polygon",points);
                            ShapesId ++;
                            C = new Canvas(caretaker.GetState().canvas.GetList());
                            C.Shapes.Add(polyline);
                            caretaker.o.NewState(C);
                            caretaker.BackUp();
                        }
                        catch (Exception e)
                        {
                            // Display an error message
                            Console.WriteLine($"ERROR: Failed to create a Polyline from ({argument})");
                            Console.WriteLine($"Exception: {e.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Failed to create a Polyline from ({argument})");
                        Console.WriteLine("Must have and X and Y for each point!");
                    }
                break;

                case "path":
                    argument = argument.Substring( 1, argument.Length - 2);
                    Path path = new Path(ShapesId, argument);
                    ShapesId++;
                    C = new Canvas(caretaker.GetState().canvas.GetList());
                    C.Shapes.Add(path);
                    caretaker.o.NewState(C);
                    caretaker.BackUp();
                break;

                default:
                    Console.WriteLine($"Could not understand \"{token} {argument}\"");
                break;
            }

            return "";
        }
    }
}