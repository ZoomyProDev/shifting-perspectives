using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOne : MonoBehaviour
{
    public bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasBeenTriggered = true;
            Debug.Log("TRIGGER");
        }
    }
}
