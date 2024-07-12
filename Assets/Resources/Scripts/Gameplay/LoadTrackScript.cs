using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT SHOULD BE ATTATCHED THE THE OUTCONNECTOR OF EACH TRACK PIECE, WHICH WILL THEN HAVE THE TRIGGER COMPONENT

public class LoadTrackScript : MonoBehaviour
{
    //Variables
    RandomGenerationScript RandomGenerationScript;

    //Awake
    private void Awake()
    {
        RandomGenerationScript = GameObject.FindGameObjectWithTag("Player").GetComponent<RandomGenerationScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Load");
            RandomGenerationScript.StartGeneration();
        }
    }
}
