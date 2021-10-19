using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;
using UnityEngine.SceneManagement;

public class RayHitsController : MonoBehaviour
{
    //右手用
    [SerializeField]
    SteamVR_LaserPointer _leftPointer;
    //左手用
    [SerializeField]
    SteamVR_LaserPointer _rightPointer;
    public string sceneName;


    void Awake()
    {
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
        if (e.target.name == "InputSubNum")
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log("Scene Changed");
        }
    }

    //レーザーポインターがtargetに触れたとき
    public void PointerInside(object sender, PointerEventArgs e)
    {
        Image ButtonImage = GetComponent<Image>();
        Color ButtonColor = ButtonImage.color;
        if (e.target.name == "InputSubNum")
        {
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 0.5f);
        }
    }

    //レーザーポインターがtargetから離れたとき
    public void PointerOutside(object sender, PointerEventArgs e)
    {
        Image ButtonImage = GetComponent<Image>();
        Color ButtonColor = ButtonImage.color;
        if (e.target.name == "InputSubNum")
        {
            ButtonImage.color = new Color(ButtonColor.r, ButtonColor.g, ButtonColor.b, 1.0f);
        }
    }
}

