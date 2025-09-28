using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor playerMotor;
    private PlayerLook playerLook;

    private WholeSceneManager WSM;


    // Start is called before the first frame update
    void Awake()
    {
        WSM = GameObject.Find("WholeSceneManager").GetComponent<WholeSceneManager>();
        Cursor.visible = false;
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        playerMotor = GetComponent<PlayerMotor>();

        playerLook = GetComponent<PlayerLook>();

        //there are three types of states that our actions have, performed, started, canceled
        //ctx is a callback context function that points to our function inside our playerMotor script

        Debug.Log(WSM);
        onFoot.Pause.performed += ctx => WSM.Pause();

        if (playerMotor != null)
        {
            onFoot.Jump.performed += ctx => playerMotor.Jump();

            onFoot.Crouch.performed += ctx => playerMotor.Crouch();
            onFoot.Sprint.performed += ctx => playerMotor.Sprint();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movement action
        if (playerMotor != null)
        {
            playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
        
 
    }

    void LateUpdate()
    {

        if (playerLook != null)
        {
            playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
        
        

    }

    private void OnEnable()
    {
        
        onFoot.Enable();

    }

    private void OnDisable()
    {
        
        onFoot.Disable();

    }
}
