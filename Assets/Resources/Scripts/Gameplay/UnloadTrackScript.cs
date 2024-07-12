using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT SHOULD BE ATTATCHED TO THE PARENT OF EACH TRACK PIECE (IE THE PREFAB THAT IS INSTANTIATED)

public class UnloadTrackScript : MonoBehaviour
{
    //Variables
    RandomGenerationScript RandomGenerationScript;

    //Awake
    private void Awake()
    {
        RandomGenerationScript = GameObject.FindGameObjectWithTag("Player").GetComponent<RandomGenerationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Unloads the track piece if it is a certain number of segments behind the most recently generated one
        if (int.Parse(gameObject.name)  <= RandomGenerationScript.GenCount - 20)
        {
            Debug.Log("Unload");
            Destroy(gameObject);
        }
    }
}
