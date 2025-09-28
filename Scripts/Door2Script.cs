using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Script : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    public bool locked = false;

    private Animator doorAnim;


    public bool noPrompt = false;


    public AudioSource audioSource;

    public AudioClip Open;
    public AudioClip Close;

    // Start is called before the first frame update
    void Start()
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

        doorAnim = gameObject.GetComponent<Animator>();
        doorOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!noPrompt)
        {
            if (!locked)
            {
                if (doorOpen)
                {
                    promptMessage = "Close Door [E]";
                }
                else
                {
                    promptMessage = "Open Door [E]";
                }
            }
            else
            {
                promptMessage = "Locked!";
            }
        }
        else
        {
            promptMessage = "";
        }
        

    }

    public override void Interact()
    {
        if (!locked)
        {
            if (noPrompt)
            {
                noPrompt = false;
            }

            Debug.Log("Interacted with: " + gameObject.name);
            if (!doorOpen)
            {
                audioSource.PlayOneShot(Open);
                doorAnim.Play("NewDoorOpen", 0, 0.0f);
                doorOpen = true;
            }
            else
            {
                audioSource.PlayOneShot(Close);
                doorAnim.Play("NewDoorClose", 0, 0.0f);
                doorOpen = false;

            }
        }



    }
}