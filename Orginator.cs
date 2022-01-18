namespace SvgCanvas
{
    /*  Orginator class, the orginator will track the currently active state
        as well as providing methods for retrieving the currently active 
        state.  */
    public class Originator
    {
        public Canvas state;    // Currently active state

        // Constructor, takes in the first state, in this case a blank canvas
        public Originator(Canvas cavnas)
        {
            this.state = cavnas;
        }

        // Update the active state
        public void NewState(Canvas cavnas)
        {
            this.state = cavnas;
        }
        
        // Return the active state in a Memento
        public Memento GetMemento()
        {
            return new Memento(this.state);
        }
    }
}