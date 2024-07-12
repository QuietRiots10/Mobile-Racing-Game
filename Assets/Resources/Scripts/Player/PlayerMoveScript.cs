using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    //Variables
    Rigidbody PlayerBody;
    public RandomGenerationScript GenScript;
    public ParticleSystem Fire1;
    public ParticleSystem Fire2;
    public AudioClip AccelerateSound;
    AudioSource AudioSource;
    public float MoveSpeedCap;

    //Start
    private void Start()
    {
        PlayerBody = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    //Update
    private void FixedUpdate()
    {
        //Accelerate the player until they reach the velocity cap
        if (PlayerBody.velocity.magnitude < MoveSpeedCap)
        {
            PlayerBody.AddForce(transform.forward * 50f, ForceMode.Acceleration);
        }

        //Change max speed at fixed intervals
        if (GenScript.GenCount > 18 && MoveSpeedCap < 18)
        {
            StartCoroutine(Accelerate(18));
        }
        if (GenScript.GenCount > 40 && MoveSpeedCap < 20)
        {
            StartCoroutine(Accelerate(20));
        }
        if (GenScript.GenCount > 60 && MoveSpeedCap < 22)
        {
            StartCoroutine(Accelerate(22));
        }
        if (GenScript.GenCount > 80 && MoveSpeedCap < 24)
        {
            StartCoroutine(Accelerate(24));
        }
        if (GenScript.GenCount > 100 && MoveSpeedCap < 26)
        {
            StartCoroutine(Accelerate(26));
        }
        if (GenScript.GenCount > 120 && MoveSpeedCap < 28)
        {
            StartCoroutine(Accelerate(28));
        }
    }

    private IEnumerator Accelerate(int speed)
    {
        MoveSpeedCap = speed;
        Debug.Log("Speeding Up: " + speed);
        Fire1.Play();
        Fire2.Play();

        AudioSource.clip = AccelerateSound;
        AudioSource.volume = 0.4f;
        //AudioSource.Play();

        yield return null;
    }
}
