using System;
using System.Collections.Generic;

/*  The Memento Design Pattern is made up of three objects, the 
    caretaker, the originator and a memento

    This is the CareTaker class part of the Memento Design Pattern. 
    The caretaker is responsible for keeping a history of states
    that an object has taken, in this case a Canvas. The states are
    wrapped in a Memento object.
    
    The caretaker will provide functionality to add new states and to 
    roll back to previously saved states aswell as rolling formward. 
    You can also retrieve the current state from the caretaker.

    The Originator is responsible for taking in new states, aswell as
    returning the current state wrapped in a Memento. When the caretaker 
    adds a new state to the history it does so via the orginator, ie.
    it will add the originators current state to the history. 

    Usually the caretaker would not directly contain the orginator, at 
    least not in the examples I saw, but for me it worked best to have 
    it this way, usually a client would contain both the caretaker and 
    orginator the pass the orginator to the caretaker. This might work 
    out better if the objects being stored were simpler.

    Finally the Memento is responsible for storing a single state, we put 
    a state in a Memento and then put that in a list called the history.
    The actual Memento can be thought of as a box storing a canvas. Then 
    the caretaker has a warehouse of the boxes and can pick and choose 
    from them.

    I'm fairly sure I could acheive the same results by ommiting the 
    Memento class entierly and instead keep a list of canvases. 
*/
namespace SvgCanvas
{
    public class Caretaker
    {
        public int index;   // Track the current state
        public Originator o{get; set;}  // The Orginator
        public List<Memento> savedMementos; // The history of the past states 

        // Constructor takes in the first state, in this case it will always be a 
        // blank canvas.  
        public Caretaker(Canvas c)
        {
            savedMementos = new List<Memento>();    // Initialise the list 
            o = new Originator(c);  // Set the first state in the orginator
            index = -1; // Index is -1 because the list is empty, when BackUp() is called the fisrt time index will the be 0
        }

        // Add a new state to the history
        public void BackUp()
        {
            // If we undo then add a new state we must first remove the dead states
            // think of this a chopping off the head of a snake, everything that 
            // comes after this new state should be removed 
            if(index < savedMementos.Count - 1)
            {
                for(int i=savedMementos.Count - 1; i>index; i--)
                {
                    savedMementos.RemoveAt(i);  // Removes the dead states
                }
            }

            savedMementos.Add(o.GetMemento());  // Add the new state to the history
            index ++; // Increment the index
        }

        // Returns the currently active state
        public Memento GetState()
        {
            return savedMementos[index];
        }

        // If possible, go back one state
        public void Undo()
        {
            // If the index is greater than 0 this means we have changes to undo
            if(index > 0)
            {
                index --;   // Decrement the index
                o.NewState(savedMementos[index].canvas);    // Update the Orginators current state
                Console.WriteLine("undone");    // Give confirmation to the user, this will only be seen in CLI Mode as the screen is constantly being redrawn in Menu Mode
            }
        }

        // If possible go forward one state
        public void Redo()
        {
            // If index is less than the index of the last item in the list then we can redo
            if(index < savedMementos.Count -1)
            {
                index ++;   // Increment index
                o.NewState(savedMementos[index].canvas);    // Update the current state in the Originator
                Console.WriteLine("redone");    // Give confirmation to the user, again only visible in CLI Mode
            }
        }
    }
}