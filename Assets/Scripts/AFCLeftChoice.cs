using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class AFCLeftChoice : MonoBehaviour
{
    [SerializeField]
    SteamVR_LaserPointer _leftPointer;

    [SerializeField]
    SteamVR_LaserPointer _rightPointer;
    LogTest LogTest;
    RandomizeGain RandG;
    EndTrialButton EndTrial;

    [SerializeField]
    string SceneName;

    void Awake()
    {
        _leftPointer.PointerIn += PointerInside;
        _leftPointer.PointerOut += PointerOutside;
        _leftPointer.PointerClick += PointerClick;
        _rightPointer.PointerIn += PointerInside;
        _rightPointer.PointerOut += PointerOutside;
        _rightPointer.PointerClick += PointerClick;
        LogTest = FindObjectOfType<LogTest>();
        RandG = FindObjectOfType<RandomizeGain>();
        EndTrial = FindObjectOfType<EndTrialButton>();
        this.gameObject.SetActive(false);
    }

    //レーザーポインターをtargetに焦点をあわせてトリガーをひいたとき
    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TwoAFCLeft")
        {
            this.LogTest.logger.LogFormat(LogType.Log, "2AFC : Left");
            
            if (RandG.trialcnt < RandG.Gain.Count)
            {
                RandG.trialcnt++;
                SceneManager.LoadScene(SceneName);
            }
            else{
                SceneManager.LoadScene("end");
            }
        }
    }

    //レーザーポインターがtargetに触れたとき
    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TwoAFCLeft")
        {
            Image ButtonImage = GetComponent<Image>();
            Color ButtonColor = ButtonImage.color;
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 0.5f);
        }
    }

    //レーザーポインターがtargetから離れたとき
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "TwoAFCLeft")
        {
            Image ButtonImage = GetComponent<Image>();
            Color ButtonColor = ButtonImage.color;
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 1.0f);
        }
    }
}
