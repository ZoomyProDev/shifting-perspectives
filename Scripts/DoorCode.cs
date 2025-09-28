using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DoorCode : Interactable
{
    public GameObject BG;
    public GameObject Input;
    public GameObject SubmitButton;
    public GameObject ExitButton;
    public GameObject Correct;
    public GameObject Incorrect;

    public GameObject promptText;

    public GameObject finalDoor;

    public bool hasBeenUsed = false;


    public AudioSource audioSource;
    public AudioClip Click;


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

    public void Update()
    {
        if (hasBeenUsed)
        {
            promptMessage = "";
        }
    }


    public override void Interact()
    {
        audioSource.PlayOneShot(Click);

        BG.SetActive(true);
        Input.SetActive(true);
        SubmitButton.SetActive(true);
        ExitButton.SetActive(true);

        promptText.SetActive(false);

        Input.GetComponent<InputField>().text = "";

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = false;

    }

    public void Submit()
    {
        if (Input.GetComponent<InputField>().text == "122234")
        {
            StartCoroutine(EndingDoor());
        }
        else
        {
            StartCoroutine(Wrong());
        }
    }

    public void Exit()
    {

        Correct.SetActive(false);
        BG.SetActive(false);
        Input.SetActive(false);
        SubmitButton.SetActive(false);
        ExitButton.SetActive(false);

        promptText.SetActive(true);

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = true;

        
    }

    IEnumerator Wrong()
    {
        Incorrect.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Incorrect.SetActive(false);
        yield return null;

    }

    IEnumerator EndingDoor()
    {
        Correct.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        finalDoor.GetComponent<Door2Script>().locked = false;
        finalDoor.GetComponent<Door2Script>().noPrompt = false;

        promptText.SetActive(true);
        Correct.SetActive(false);
        BG.SetActive(false);
        Input.SetActive(false);
        SubmitButton.SetActive(false);
        ExitButton.SetActive(false);

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        hasBeenUsed = true;

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = true;

        yield return null;


    }
}
