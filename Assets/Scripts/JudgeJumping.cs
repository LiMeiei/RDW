
using System;
using UnityEngine;

public class JudgeJumping : MonoBehaviour
{
    [SerializeField]
    TrackerCalibration StandStatus; 

    [SerializeField]
    TrackingTracker TrTck; 

    [SerializeField]
    GameObject World;

    [SerializeField]
    GameObject DestoryObjects;

    [SerializeField]
    GameObject eye;

    [SerializeField]
    GameObject Scaffold;

    [SerializeField]
    GameObject TargetScaffold;


    [SerializeField]
    AFCLeftChoice AFCLeft;

    [SerializeField]
    AFCRightChoice AFCRight;

    [SerializeField]
    AFCChoiceText AFCtext;


    LogTest LogTest;
    RandomizeGain RandG;
    DisplayJumpingStatus displayJumpingStatus;

    BoxDisplay boxDisplay;


    [HideInInspector]
    public bool IsJumping;

    
    



    float gain;
    float height;
    float LastPosZ;
    
    [HideInInspector]
    public float StartPosZ;
    float ApplyGain = 0; // 一回の跳躍で適用したゲインの合計
    bool IsLFup = false, IsRFup = false, IsWaistup = false;
    Color boxcolor; // 現在のユーザの状態を取得

    [HideInInspector]
    public int jumpcnt = 0; // 何回ジャンプしたのか
    float eyeMoveDis = 0; // １フレームの間に動いた距離






    public void Start()
    {
        RandG = FindObjectOfType<RandomizeGain>();
        LogTest = FindObjectOfType<LogTest>();
        displayJumpingStatus = FindObjectOfType<DisplayJumpingStatus>();
        

        gain = RandG.Gain[RandG.trialcnt - 1].Item1;
        height = RandG.Gain[RandG.trialcnt - 1].Item2;
        IsJumping = false;
        this.LogTest.logger.LogFormat(LogType.Log, "TrialNum: " + RandG.trialcnt + "回目　,Gain: " + gain + " ,Height: " + height);
        LastPosZ = eye.transform.position.z;
        // StartPosZ = LastPosZ;
        Debug.Log("StartPosZ: "+ StartPosZ);

        this.gameObject.SetActive(false);
    }



    public void Update()
    {
        float LFMove = TrTck.TrackerPosition_LeftFoot.y - StandStatus.LFIniPos;
        float RFMove = TrTck.TrackerPosition_RightFoot.y - StandStatus.RFIniPos;
        float WaistMove = TrTck.TrackerPosition_Waist.y - StandStatus.WaistIniPos;

        if(LFMove >= 0.1f){
            if(!IsLFup){
            }
            IsLFup = true;
        }else{
            if(IsLFup){
            }
            IsLFup = false;
        }
        if(RFMove >= 0.1f){
            if(!IsRFup){
            }
            IsRFup = true;
        }else{
            if(IsRFup){
            }
            IsRFup = false;
        }
        if(WaistMove >= 0.1f){
            if(!IsWaistup){
            }
            IsWaistup = true;
        }else{
            if(IsWaistup){
            }
            IsWaistup = false;
        }

        if(IsLFup && IsRFup && IsWaistup){
            if(!IsJumping){
                Debug.Log("Jumping");
            }
            IsJumping = true;
        }
        else{
            if(IsJumping){
                Debug.Log("Descending");
            }
            IsJumping = false;
        }

        eyeMoveDis = Math.Abs(eye.transform.position.z - LastPosZ);
        float speed = eyeMoveDis / Time.deltaTime;



        boxcolor = displayJumpingStatus.gameObject.GetComponent<Renderer>().material.color;

        /*１フレームのプログラム*/
        float FrameGain;

        if (boxcolor == Color.yellow)
        {
            FrameGain = gain * 0.003f * speed;
        }
        else if (boxcolor == Color.green)
        {
            FrameGain = gain * 0.06f * speed;
        }
        else if (boxcolor == Color.blue)
        {
            FrameGain = gain * 0.012f * speed;
        }
        else{
            FrameGain = 0; // 一回のジャンプの適用量にすでに達していたら終了
        }

        ApplyGain += FrameGain;


        /* 適用量を超えていたら適用しない */
        if (Math.Abs(ApplyGain) - Math.Abs(gain) >= 0)
        {
            displayJumpingStatus.gameObject.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("box is red");
            Debug.Log("movedistance: " + Mathf.Abs(eye.transform.position.z - StartPosZ));
        }

        /* 次のジャンプまでの待機解除条件 */
        if (boxcolor == Color.red && Mathf.Abs(eye.transform.position.z - StartPosZ) >= 1f)
        {
            jumpcnt++;
            ApplyGain = 0;
            StartPosZ = eye.transform.position.z;
            Debug.Log("StartPosZ: " +  StartPosZ);
            displayJumpingStatus.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            this.LogTest.logger.LogFormat(LogType.Log, "jumpcnt: " + jumpcnt);
            this.LogTest.logger2.LogFormat(LogType.Log, "jumpcnt: " + jumpcnt);

            boxDisplay = FindObjectOfType<BoxDisplay>();
            boxDisplay.gameObject.GetComponent<Renderer>().material.color = new Color(55, 120, 248, 255);


            /*合計三回ジャンプしたら試行終了*/
            if (jumpcnt == 3)
            {
                AFCLeft.gameObject.SetActive(true);
                AFCRight.gameObject.SetActive(true);
                AFCtext.gameObject.SetActive(true);
                this.LogTest.logger.LogFormat(LogType.Log, "TrialNum: " + RandG.trialcnt + "回目の操作が終了");
                this.gameObject.SetActive(false);
            }
            
            Scaffold.gameObject.transform.position += new Vector3(0, 0, -1.0f);
            TargetScaffold.gameObject.transform.position += new Vector3(0, 0, -1.0f);
            boxDisplay.gameObject.transform.position += new Vector3(0, 0, -1.0f);
        }



        World.transform.RotateAround(eye.transform.position, Vector3.up, FrameGain);
        // Debug.Log("FrameGain, :" + FrameGain);
        // Debug.Log("ApplyGain, :" + ApplyGain);



        LastPosZ = eye.transform.position.z;
    }
}
