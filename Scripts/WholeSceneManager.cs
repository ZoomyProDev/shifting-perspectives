using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class WholeSceneManager : MonoBehaviour
{
    //This file manages major events across the game's playthrough


    public bool inTransfer;

    public bool firstTransferDone = false;

    public bool canEnterOutlet = false;

    public GameObject TriggerOne;
    public GameObject TriggerTwo;
    public GameObject TriggerThree;
    public GameObject TriggerFour;
    public GameObject TriggerFive;
    public GameObject TriggerEnd;


    public bool Trigger1Done = false;
    public bool Trigger2Done = false;
    public bool Trigger3Done = false;
    public bool Trigger4Done = false;
    public bool Trigger5Done = false;
    public bool TriggerEndDone = false;


    public GameObject pausedBG;
    public GameObject pausedTitle;
    public GameObject resume;
    public GameObject home;
    public GameObject hintTitle;
    public GameObject hintText;
    public GameObject promptText;
    public GameObject narrationText;
    public GameObject crosshair;

    public GameObject levelChanger;

    public AudioSource audioSource;

    public AudioClip emptyClip;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    public AudioClip clip8;
    public AudioClip clip9;
    public AudioClip clip10;
    public AudioClip clip11;


    private void Start()
    {
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        pausedBG.SetActive(false);
        pausedTitle.SetActive(false);
        resume.SetActive(false);
        home.SetActive(false);
        hintTitle.SetActive(false);
        hintText.SetActive(false);

        promptText.SetActive(true);
        narrationText.SetActive(true);
        crosshair.SetActive(true);

        hintText.GetComponent<Text>().text = "Try looking at the basketball.";
        StartCoroutine(NarrationLine("You might be wondering where you are. Let's just say it's time to see things in a different light, it's time to shift your perspective.", "Use your mouse to look around and try to shift yourself into the basketball in front of you.", false, clip1, clip2));

    }

    private void Update()
    {

        if (TriggerOne.GetComponent<TriggerOne>().hasBeenTriggered == true && Trigger1Done == false)
        {


            hintText.GetComponent<Text>().text = "Try shifting into something small to fit through the hole in the door...";

            StartCoroutine(NarrationLine("Sometimes we make problems seem much bigger then they are; sometimes the solution is quite small.", "If you are stuck, you can always access the pause menu for a hint by pressing P.", false, clip4, clip10));


            Trigger1Done = true;
        }
        if (TriggerTwo.GetComponent<TriggerTwo>().hasBeenTriggered == true && Trigger2Done == false)
        {


            hintText.GetComponent<Text>().text = "Maybe something bigger could jump up those stairs...";

            StartCoroutine(NarrationLine("Challanges often demand a shift in your mindset, and sometimes, you must think about them from the opposite perspective to what you are used to.", "", false, clip5, emptyClip));


            Trigger2Done = true;
        }
        if (TriggerThree.GetComponent<TriggerThree>().hasBeenTriggered == true && Trigger3Done == false)
        {


            hintText.GetComponent<Text>().text = "There must be a switch to turn on the power for the outlet somewhere...";

            StartCoroutine(NarrationLine("You can't always power through all of your problems, sometimes you need to take a break or even backtrack a bit.", "", false, clip6, emptyClip));



            Trigger3Done = true;
        }
        if (TriggerFour.GetComponent<TriggerFour>().hasBeenTriggered == true && Trigger4Done == false)
        {


            hintText.GetComponent<Text>().text = "I wonder if that door is locked?";

            StartCoroutine(NarrationLine("Not all problems are so complicated, sometimes you are just overthinking things.", "", false, clip11, emptyClip));



            Trigger4Done = true;
        }
        if (TriggerFive.GetComponent<TriggerFive>().hasBeenTriggered == true && Trigger5Done == false)
        {

            hintText.GetComponent<Text>().text = "There are six rooms in this house. Maybe try and find all the paintings?";

            StartCoroutine(NarrationLine("Sometimes you have to backtrack a little and look at things with a new perspective. Like they say, sometimes you just have to stop and smell the paintings", "", false, clip7, emptyClip));


            Trigger5Done = true;
        }
        if (TriggerEnd.GetComponent<TriggerEnd>().hasBeenTriggered == true && TriggerEndDone == false)
        {


            hintText.GetComponent<Text>().text = "You're all done! Go out onto the balcony.";


            GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = false;
            GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = false;


            TriggerEndDone = true;

            StartCoroutine(NarrationLine("Now with just a few examples, hopefully you have realized that sometimes all it takes to solve your problems is to shift your perspective.", "", true, clip8, emptyClip));


        }
    }

    public void Resume()
    {

        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;


        pausedBG.SetActive(false);
        pausedTitle.SetActive(false);
        resume.SetActive(false);
        home.SetActive(false);
        hintTitle.SetActive(false);
        hintText.SetActive(false);

        promptText.SetActive(true);
        narrationText.SetActive(true);
        crosshair.SetActive(true);

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = true;
    }


    public void Home()
    {
        levelChanger.GetComponent<LevelChanger>().FadeToLevel("Start");
    }

    public void Pause()
    {
        Debug.Log("Pause");

        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        pausedBG.SetActive(true);
        pausedTitle.SetActive(true);
        resume.SetActive(true);
        home.SetActive(true);
        hintTitle.SetActive(true);
        hintText.SetActive(true);

        promptText.SetActive(false);
        narrationText.SetActive(false);
        crosshair.SetActive(false);

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerLook>().canLook = false;
    }


    public IEnumerator NarrationLine(string line, string line2, bool end, AudioClip clip, AudioClip clip2)
    {

        audioSource.Stop();

        audioSource.PlayOneShot(clip);
        narrationText.GetComponent<Text>().text = line;
        

        yield return new WaitForSecondsRealtime(clip.length);

        if (line2 != "")
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip2);
            narrationText.GetComponent<Text>().text = line2;

            yield return new WaitForSecondsRealtime(clip2.length);
        }
        else
        {
            narrationText.GetComponent<Text>().text = "";
        }

        if (end)
        {
            narrationText.GetComponent<Text>().text = "";
            levelChanger.GetComponent<LevelChanger>().FadeToLevel("Credits");
        }

        narrationText.GetComponent<Text>().text = "";

        yield return null;
    }
}
