using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class EndTrialButton : MonoBehaviour
{
    [SerializeField]
    SteamVR_LaserPointer _leftPointer;

    [SerializeField]
    SteamVR_LaserPointer _rightPointer;
    LogTest LogTest;

    bool IsDebug = false;

    void Start(){
        LogTest = FindObjectOfType<LogTest>();
    }

    void Update(){
        if(!IsDebug){
            this.LogTest.logger.LogFormat(LogType.Log, "全試行終了");
            IsDebug = true;
        }
    }
}
