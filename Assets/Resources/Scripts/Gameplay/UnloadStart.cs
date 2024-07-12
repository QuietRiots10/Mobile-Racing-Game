using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadStart : MonoBehaviour
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
        if (RandomGenerationScript.GenCount >= 20)
        {
            Destroy(gameObject);
        }
    }
}
