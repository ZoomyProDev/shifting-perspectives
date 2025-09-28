using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    [SerializeField]
    private GameObject item;
    private bool itemState;
    // Start is called before the first frame update
    void Start()
    {
        itemState = item.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemState)
        {
            promptMessage = "Turn Off [E]";
        }
        else
        {
            promptMessage = "Turn On [E]";
        }
    }

    public override void Interact()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        itemState = !itemState;
        item.SetActive(itemState);
    }
}
