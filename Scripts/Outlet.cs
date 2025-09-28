using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlet : Interactable
{
    [SerializeField]
    private GameObject item;

    public bool hasBeenUsed = false;

    public GameObject lamp;

    public WholeSceneManager WSM;

    public GameObject TriggerFour;

    public AudioSource audioSource;
    public AudioClip Shift;

    private void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SFX", 1) == 0)
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenUsed)
        {
            if (WSM.canEnterOutlet)
            {
                promptMessage = "Shift [E]";
            }
            else if (WSM.Trigger3Done)
            {
                promptMessage = "No Power!";
            }
        }
        else
        {
            promptMessage = "";
        }
        

    }

    public override void Interact()
    {
        if (!hasBeenUsed)
        {
            if (WSM.canEnterOutlet)
            {
                hasBeenUsed = true;
                audioSource.PlayOneShot(Shift);
                TriggerFour.GetComponent<TriggerFour>().hasBeenTriggered = true;
                Debug.Log("Interacted with: " + gameObject.name);
                lamp.GetComponent<TransferCube>().Interact();

            }
        }
        

    }
}
