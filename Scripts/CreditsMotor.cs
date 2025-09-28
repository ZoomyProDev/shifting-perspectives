using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMotor : MonoBehaviour
{

    public float velocity;

    public GameObject levelChanger;


    // Update is called once per frame


    private void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {

        if (gameObject.transform.position.y >= 16.5f)
        {
            levelChanger.GetComponent<LevelChanger>().FadeToLevel("Start");
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + velocity, gameObject.transform.position.z);
        }
        
    }
}
