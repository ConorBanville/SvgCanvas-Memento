namespace SvgCanvas
{
    /*  Memento Class, contains a state, in this case a Canvas.
        I have a setter on the state but we should never
        really need to update a Memento. We create it,
        store it and possibly delete it. That's all.*/
    public class Memento
    {
        public Canvas canvas{get; set;} // The state to be stored

        // Constructor, passed a state 
        public Memento(Canvas state)
        {
            this.canvas = state;
        }
    }
}