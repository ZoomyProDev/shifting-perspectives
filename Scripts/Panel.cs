using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : Interactable
{
    [SerializeField]
    private GameObject item;
    private bool itemState;

    public WholeSceneManager WSM;

    public AudioSource audioSource;
    public AudioClip Click;

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

        itemState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemState && WSM.Trigger3Done)
        {
            promptMessage = "Use Power Switch [E]";
        }
        else if (WSM.Trigger3Done)
        {
            promptMessage = "On";
        }

    }

    public override void Interact()
    {
        if (!itemState && WSM.Trigger3Done)
        {
            audioSource.PlayOneShot(Click);
            Debug.Log("Interacted with: " + gameObject.name);
            itemState = !itemState;
            WSM.canEnterOutlet = true;
        }
        
    }
}
