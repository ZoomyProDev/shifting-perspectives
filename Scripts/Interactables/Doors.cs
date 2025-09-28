using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    public bool locked = false;

    public bool outletDoor;

    private Animator doorAnim;

    private WholeSceneManager WSM;

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
        WSM = GameObject.Find("WholeSceneManager").GetComponent<WholeSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!outletDoor)
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

            if (WSM.Trigger4Done)
            {
                locked = false;
            }
        }

        
        
    }

    public override void Interact()
    {
        if (!locked)
        {
            Debug.Log("Interacted with: " + gameObject.name);
            if (!doorOpen)
            {
                audioSource.PlayOneShot(Open);
                doorAnim.Play("DoorOpen", 0, 0.0f);
                doorOpen = true;
            }
            else
            {
                audioSource.PlayOneShot(Close);
                doorAnim.Play("DoorClose", 0, 0.0f);
                doorOpen = false;

            }
        }
        
        
        
    }
}