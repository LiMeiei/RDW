using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

public class TrackingTracker : MonoBehaviour
{
    [HideInInspector]
    public Vector3 TrackerPosition_LeftFoot, TrackerPosition_RightFoot, TrackerPosition_Waist;

    [HideInInspector]
    public SteamVR_Action_Pose TrackerPose = SteamVR_Actions.default_Pose;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        TrackerPosition_LeftFoot = TrackerPose.GetLocalPosition(SteamVR_Input_Sources.LeftFoot);
        TrackerPosition_RightFoot = TrackerPose.GetLocalPosition(SteamVR_Input_Sources.RightFoot);
        TrackerPosition_Waist = TrackerPose.GetLocalPosition(SteamVR_Input_Sources.Waist);
        // Debug.Log("LeftFoot Position: " + TrackerPosition_LeftFoot.x +  ", " + TrackerPosition_LeftFoot.y + ", " + TrackerPosition_LeftFoot.z);
        // Debug.Log("RightFoot Position: " + TrackerPosition_RightFoot.x + ", " + TrackerPosition_RightFoot.y + ", " + TrackerPosition_RightFoot.z);
        Debug.Log("Waist Position: " + TrackerPosition_Waist.x + ", " + TrackerPosition_Waist.y + ", " + TrackerPosition_Waist.z);
    }
}
