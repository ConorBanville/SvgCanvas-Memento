/*      CS264 Assignment 4
        ------------------

        For the assignment I decided to command line interface, allowing a user to
        create a canvas, add shapes to the canvas, update / remove shapes aswell as exporting
        the canvas to a svg file.

        In order to implemenent undo-redo functionallity I used the memento design pattern,
        with a slight variation. I will outline exactly how it was implemented in the relevant
        class files, with a more broad overview explanation in the CareTaker.cs file.

        As you will see below the Program class is pretty much bare containing only a Manager
        object and calling Start() on it. Essentially, the manager is just a large switch 
        statment that contains cases for each possible input from the user.

        Name: Conor Banville
        Student Number: 18383803
        OS : Windows 10/11
        IDE: Visual Studio Code (I ported the project over to VS 2019 just to use the Class Diagram extension at the end, aswell as CodeMaid)
*/

namespace SvgCanvas
{
    class Program
    {
        public static Manager m = new Manager();

        static void Main(string[] args)
        {
            m.Start();
        }
    }
}
