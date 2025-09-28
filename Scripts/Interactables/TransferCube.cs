using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TransferCube : Interactable
{

    public PostProcessingManager p;

    public ObjectInfo objectInfo;

    public WholeSceneManager WholeSceneManager;

    public AudioSource audioSource;
    public AudioClip Shift;


    private bool destroyPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("SFX", 1) == 0)
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }
        objectInfo = GetComponent<ObjectInfo>();
        WholeSceneManager = GameObject.Find("WholeSceneManager").GetComponent<WholeSceneManager>();
        
        Shift = objectInfo.Shift;
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator TransferWait()
    {

        StartCoroutine(p.Transfer());


        yield return new WaitForSecondsRealtime(1f);

        gameObject.SetActive(false);

        Destroy(gameObject.GetComponent<Rigidbody>());

        gameObject.AddComponent<CharacterController>();
        gameObject.GetComponent<CharacterController>().stepOffset = (((transform.localScale.x * gameObject.GetComponent<CharacterController>().height) + ((transform.localScale.x * gameObject.GetComponent<CharacterController>().radius) * 2)) / 10);
        gameObject.SetActive(true);

        StartCoroutine(TransferFinish());
        

        yield return null;
        
    }


    public override void Interact()
    {
        if (!WholeSceneManager.firstTransferDone)
        {
            WholeSceneManager.firstTransferDone = true;
            destroyPlayer = true;
            WholeSceneManager.StartCoroutine(WholeSceneManager.NarrationLine("Great! You can use the W, A, S, and D keys to move around. You can also use the spacebar to jump!", "", false, WholeSceneManager.clip9, WholeSceneManager.emptyClip));

        }

        GameObject.FindWithTag("Player").GetComponent<PlayerMotor>().canMove = false;

        audioSource.PlayOneShot(Shift);
        StartCoroutine(TransferWait());

    }

    public IEnumerator TransferFinish()
    {
        transform.localEulerAngles = new Vector3(objectInfo.rotation.x, objectInfo.rotation.y, objectInfo.rotation.z);
        gameObject.AddComponent<PlayerMotor>();
        gameObject.GetComponent<PlayerMotor>().speed = (transform.localScale.x + transform.localScale.y + transform.localScale.z) * 1.05f;
        gameObject.GetComponent<PlayerMotor>().jumpHeight = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 6;
        gameObject.GetComponent<PlayerMotor>().WholeSceneManager = GameObject.Find("WholeSceneManager").GetComponent<WholeSceneManager>();
        gameObject.AddComponent<PlayerLook>();
        gameObject.AddComponent<PlayerInteract>();
        gameObject.AddComponent<PlayerUI>();
        GameObject.FindWithTag("Camera").transform.position = new Vector3(transform.position.x, (transform.position.y + (transform.localScale.y / 2)), transform.position.z);
        GameObject.FindWithTag("Camera").transform.parent = gameObject.transform;
        gameObject.GetComponent<PlayerLook>().cam = GameObject.FindWithTag("Camera").GetComponent<Camera>();
        gameObject.GetComponent<PlayerInteract>().mask = 1 << 6;
        gameObject.GetComponent<PlayerUI>().promptText = GameObject.Find("PromptText").GetComponent<Text>();
        gameObject.AddComponent<InputManager>();


        StartCoroutine(p.UndoTransfer());

        yield return new WaitForSeconds(1f);



        Destroy(gameObject.GetComponent<TransferCube>());

        GameObject.FindWithTag("Player").AddComponent<TransferCube>();
        GameObject.FindWithTag("Player").GetComponent<TransferCube>().promptMessage = "Shift [E]";
        GameObject.FindWithTag("Player").GetComponent<TransferCube>().p = GameObject.FindWithTag("PostProcessing").GetComponent<PostProcessingManager>();
        GameObject.FindWithTag("Player").GetComponent<TransferCube>().WholeSceneManager = GameObject.Find("WholeSceneManager").GetComponent<WholeSceneManager>();
        GameObject.FindWithTag("Player").AddComponent<Rigidbody>();
        Destroy(GameObject.FindWithTag("Player").GetComponent<CharacterController>());
        Destroy(GameObject.FindWithTag("Player").GetComponent<InputManager>());
        Destroy(GameObject.FindWithTag("Player").GetComponent<PlayerMotor>());
        Destroy(GameObject.FindWithTag("Player").GetComponent<PlayerLook>());
        Destroy(GameObject.FindWithTag("Player").GetComponent<PlayerInteract>());
        Destroy(GameObject.FindWithTag("Player").GetComponent<PlayerUI>());

        if (destroyPlayer)
        {
            Destroy(GameObject.FindWithTag("Player"));
            destroyPlayer = false;
        }
        else
        {
            GameObject.FindWithTag("Player").tag = "Untagged";
        }

        gameObject.tag = "Player";

        yield return null;
    }

}
