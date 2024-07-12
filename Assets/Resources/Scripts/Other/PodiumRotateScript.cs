using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumRotateScript : MonoBehaviour
{
    //Update
    void Update()
    {
        transform.Rotate(0, 20 * Time.deltaTime, 0);
    }
}
