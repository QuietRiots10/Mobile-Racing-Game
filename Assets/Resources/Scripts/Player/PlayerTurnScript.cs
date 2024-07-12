using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnScript: MonoBehaviour
{
    //Variables
    Rigidbody PlayerBody;
    Camera MainCamera;

    //Ranges from -1 to 1, identifies how much to turn
    public float TargetRot = 0;

    public bool AlreadyJumped = false;
    public float DefaultFOV;

    //Start
    private void Start()
    {
        PlayerBody = GetComponent<Rigidbody>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        DefaultFOV = MainCamera.fieldOfView;
    }

    private void Awake()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().velocity = transform.forward;
    }

    //FixedUpdate
    private void FixedUpdate()
    {
        //Update the target rotation angle (from -1 to 1)

        //Use mouse position (if there is a mouse present)
        if (Input.mousePresent)
        {
            TargetRot = ((Mathf.Clamp(Input.mousePosition.x, 0, Screen.width) - (Screen.width / 2)) / (Screen.width/2));

        }
        //Use touch controls (mobile)
        else
        {
            //Is the player touching the screen?
            if (Input.touchCount > 0)
            {
                Touch touch1 = Input.GetTouch(0);
                TargetRot = (Mathf.Clamp(touch1.position.x, Screen.width/4, Screen.width * 3/4) - (Screen.width / 2)) / (Screen.width / 4);

            }
            //Otherwise, InputXPos is 0
            else
            {
                TargetRot = 0;
            }
        }

        //Check if the player should jump up (Are they turning sharply and have they already jumped)
        if (Mathf.Abs(TargetRot) > 0.75 && !AlreadyJumped)
        {
            AlreadyJumped = true;
            PlayerBody.AddForce(Vector3.up * 1.5f, ForceMode.VelocityChange);
        }

        //Allow the player to jump again when they return to facing forward
        if (Mathf.Abs(TargetRot) < 0.15)
        {
            AlreadyJumped = false;
        }

        //Update player's rotation angle
        transform.localEulerAngles = new Vector3(0, transform.eulerAngles.y + 2.75f * TargetRot, 0);

        //Turn Sharply (Lowers FOV, applies slowdown)
        if (Mathf.Abs(TargetRot) > 0.75 && Time.timeScale != 0)
        {
            //Lower FOV
            MainCamera.fieldOfView -= 0.2f;

            //Slow time scale
            Time.timeScale = 0.8f;
            Time.fixedDeltaTime = 0.02f * 0.8f;
        }
        //Return FOV and slowdown to normal if you arent turning sharply
        else if (MainCamera.fieldOfView <= DefaultFOV && Time.timeScale != 0)
        {
            //Restore FOV
            MainCamera.fieldOfView += 0.5f;

            //Restore Time Scale
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
        //If FOV is somehow above normal, just set it back to default
        else
        {
            MainCamera.fieldOfView = DefaultFOV;
            //Time.timeScale = 1f;
        }

    }
}
