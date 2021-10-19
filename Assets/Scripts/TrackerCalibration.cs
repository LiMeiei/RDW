using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR.Extras;
using Valve.VR;
using System;
using UnityEngine.SceneManagement;


public class TrackerCalibration : MonoBehaviour
{

    [SerializeField]
    TrackingTracker TrTck;
    RandomizeGain RandG;

    //右手用
    [SerializeField]
    SteamVR_LaserPointer _leftPointer;
    //左手用
    [SerializeField]
    SteamVR_LaserPointer _rightPointer;

    [HideInInspector]
    public float LFIniPos, RFIniPos, WaistIniPos;

    [SerializeField]
    GameObject Eye;

    BoxDisplay boxDisplay;
    FootCenterLine footCenterLine;
    float EyeToHeadDistance = 0.1f; //目から頭上までの長さ

    [SerializeField]
    GameObject Scene;
    float StartPosX, StartPosZ;

    LogTest LogTest;
    JudgeJumping JudgeJumping;

    void Awake()
    {
        LogTest = FindObjectOfType<LogTest>();
        RandG = FindObjectOfType<RandomizeGain>();
        boxDisplay = FindObjectOfType<BoxDisplay>();
        JudgeJumping = FindObjectOfType<JudgeJumping>();
        footCenterLine = FindObjectOfType<FootCenterLine>();
        StartPosX = Eye.transform.position.x;
        StartPosZ = Eye.transform.position.z;
        _leftPointer.PointerIn += PointerInside;
        _leftPointer.PointerOut += PointerOutside;
        _leftPointer.PointerClick += PointerClick;
        _rightPointer.PointerIn += PointerInside;
        _rightPointer.PointerOut += PointerOutside;
        _rightPointer.PointerClick += PointerClick;
    }


    //レーザーポインターをtargetに焦点をあわせてトリガーをひいたとき
    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "StandCalibButton")
        {
            LFIniPos = TrTck.TrackerPosition_LeftFoot.y;
            RFIniPos = TrTck.TrackerPosition_RightFoot.y;
            WaistIniPos = TrTck.TrackerPosition_Waist.y;
            this.LogTest.logger.LogFormat(LogType.Log, 
                "LFIniPos: " + LFIniPos + " ,RFIniPos: " + RFIniPos + " ,WaistIniPos: " + WaistIniPos);
            boxDisplay.gameObject.SetActive(true);
            boxDisplay.gameObject.transform.position = 
                new Vector3(boxDisplay.gameObject.transform.position.x,
                            Eye.transform.position.y + RandG.Gain[RandG.trialcnt - 1].Item2 + EyeToHeadDistance,
                            Eye.transform.position.z - 1.0f);

            // footCenterLine.gameObject.SetActive(true);
            // footCenterLine.gameObject.transform.position =
            //     new Vector3(footCenterLine.gameObject.transform.position.x,
            //                 -0.43f,
            //                 Eye.transform.position.z - 0.5f);

            JudgeJumping.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }


    //レーザーポインターがtargetに触れたとき
    public void PointerInside(object sender, PointerEventArgs e)
    {
        Image ButtonImage = GetComponent<Image>();
        Color ButtonColor = ButtonImage.color;
        if (e.target.name == "StandCalibButton")
        {
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 0.5f);
        }
    }


    //レーザーポインターがtargetから離れたとき
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Image ButtonImage = GetComponent<Image>();
        Color ButtonColor = ButtonImage.color;
        if (e.target.name == "StandCalibButton")
        {
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 1.0f);
        }
    }
}

