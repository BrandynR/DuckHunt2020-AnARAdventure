 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.Events;
 
 public class ClickCube : MonoBehaviour {

    public string url;
 
    [SerializeField] UnityEvent anEvent; // puts an easy to setup event in the inspector and anEvent references it so you can .Invoke() it
 
    // This captures a click as long as you have a collider, even if it's set to just be a trigger, and nothing blocking it.
    // However, it will still capture even if a Gui button is on top of it, so make sure you aren't checking this while also checking Gui
    // Only other colliders block and it's not as manageable as Canvas Groups.
 
    private void OnMouseDown()
    {
        print("You clicked the cube!");
        anEvent.Invoke(); // Triggers the events you have setup in the inspector
    }
 
    // This is the first method the event is setup to do, the second audio part needed no script to just do a one shot effect, thanks to the event system.
    // You just set up the Event in the inspector for easy peasy, but the UnityEvent could also be coded the same way if needed.
    public void EventClick() // methods have to be public void to be used by UnityEvents, they can't really return anything either, as far as I know... At least I don't know how an event will capture the return...
    {
        print("Which also triggered this method as a UnityEvent!");
    }

    //When used in conjuction with functions above enables a clickable link
    public void Open()
    {
        Application.OpenURL(url);
    }
}
