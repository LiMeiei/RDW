using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayJumpingStatus : MonoBehaviour
{
    [SerializeField]
    JudgeJumping JudgeJumping;

    void Start()
    {
        // JudgeJumping = FindObjectOfType<JudgeJumping>();
        GetComponent<Renderer>().material.color = Color.yellow;
        Debug.Log("Ready");
    }


    void Update()
    {
        Color boxcolor = this.GetComponent<Renderer>().material.color;
        // Debug.Log(boxcolor);

        if(boxcolor == Color.yellow && JudgeJumping.IsJumping){
            this.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("UpDown");
        }
        if(boxcolor == Color.green && !JudgeJumping.IsJumping){
            this.GetComponent<Renderer>().material.color = Color.blue;
            Debug.Log("Landing");
        }
        
    }
}
