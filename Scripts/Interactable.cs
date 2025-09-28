using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{


    //Add or remove an InteractionEvent component to this game object
    public bool useEvents;

    //message displayed to player when looking at an interactable.
    [SerializeField]
    public string promptMessage;


    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();

        Interact();
    }

    public virtual void Interact()
    {
        //there is no code in this function
        //it is a template function to be overridden by the subclasses
    }
}
