using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkidScript : MonoBehaviour
{
    //Objects
    public Skidmarks SkidmarksController;
    WheelCollider WheelCollider;
    PlayerTurnScript TurnScript;
    WheelHit WheelHitInfo;

    //Variables

    //Index of last skidmark piece used. Set to -1 when no skidmarks are being used yet
    int lastSkid = -1;

    // Start is called before the first frame update
    void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
        TurnScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTurnScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (WheelCollider.GetGroundHit(out WheelHitInfo))
        {
            //Skid if we should (Based on how hard the player is turning)
            if (Mathf.Abs(TurnScript.TargetRot) > 0.75)
            {
                float intensity = Mathf.Abs(TurnScript.TargetRot);
                Vector3 skidPoint = WheelHitInfo.point;
                lastSkid = SkidmarksController.AddSkidMark(skidPoint, WheelHitInfo.normal, intensity, lastSkid);
            }
            else
            {
                //Reset lastSkid
                lastSkid = -1;
            }
        }
        else
        {
            //Reset lastSkid
            lastSkid = -1;
        }
    }
}
