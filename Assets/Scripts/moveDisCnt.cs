using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDisCnt : MonoBehaviour
{
    public GameObject avatar;
    public GameObject eye;

    float distance;

    float InitialDistance;
 
    /*最後にいた場所*/
    float latestz; 
    float latestx;

 
    /*1フレームの間に移動した距離*/
    [HideInInspector]
    public float MoveX,MoveZ;
 
    /*MoveX. MoveZをベクトルに*/
    public Vector3 MoveVec;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(
            avatar.transform.position.x ,
            this.transform.position.y,
            this.transform.position.z
        );
        Debug.Log("PlayerMovex : " + avatar.transform.position.x + " Guiderx : " + this.transform.position.x);
        distance =  Mathf.Abs(this.transform.position.z - avatar.transform.position.z);

        latestx = eye.transform.position.x;
        latestz = eye.transform.position.z;
    
    }

    // Update is called once per frame
    void Update()
    {
        MoveX = eye.transform.position.x - latestx;
        MoveZ = eye.transform.position.z - latestz;
        MoveVec = new Vector3(MoveX, 0, MoveZ);
        latestx = eye.transform.position.x;
        latestz = eye.transform.position.z;
    }
}
