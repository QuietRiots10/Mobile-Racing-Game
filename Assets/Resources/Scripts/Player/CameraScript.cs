using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT FACES THE CAMERA TOWARDS THE PLAYER

public class CameraScript : MonoBehaviour
{    
    //Variables
    GameObject Player;
    Rigidbody PlayerBody;
    public Vector3 CameraOffset;
    float PlayerRot;

    private Vector3 velocity = Vector3.zero;

    //Start
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerBody = Player.GetComponent<Rigidbody>();
    }

    //FixedUpdate
    private void Update()
    {
        //Change Camera Position
        transform.position = Vector3.SmoothDamp(transform.position, Player.transform.position + (Vector3.up * CameraOffset.y) + (PlayerBody.velocity.normalized * CameraOffset.z), ref velocity, 0.1f);

        //Change Camera Rotation
        Vector3 LookDirection = ((Player.transform.position + Player.transform.up * 1.5f) - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection), 10f * Time.deltaTime);
    }

}