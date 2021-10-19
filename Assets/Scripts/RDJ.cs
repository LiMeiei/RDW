using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RDJ : MonoBehaviour
{
    [SerializeField]
    public GameObject eye; //eye

    [System.NonSerialized]
    public float InitialY;

    [System.NonSerialized]
    public bool flag = true;
    RandomizeGain RandG;

    bool IsJumping = false;

    float LastPosX, LastPosZ;
    float MoveX, MoveZ;
    float Distance;

    Tuple<float, float> gain;

    LogTest LogTest;

    void Start()
    {
        RandG = FindObjectOfType<RandomizeGain>();
        LogTest = FindObjectOfType<LogTest>();
        LastPosX = eye.transform.position.x;
        LastPosZ = eye.transform.position.z;
        InitialY = eye.transform.position.y;
        // gain = RandG.Gain[RandG.trialcnt];
        
    }


    void Update()
    {
        IsJumping = FindObjectOfType<JudgeJumping>().IsJumping;
        if(IsJumping){
            Debug.Log("IsJumping");
            MoveX = eye.transform.position.x - LastPosX;
            MoveZ = eye.transform.position.z - LastPosZ;
            Distance = Mathf.Sqrt(Mathf.Pow(MoveX, 2) + Mathf.Pow(MoveZ, 2));
            this.transform.RotateAround(eye.transform.position, Vector3.up, gain.Item1);
        }
        LastPosX = eye.transform.position.x;
        LastPosZ = eye.transform.position.z;
    }
}
