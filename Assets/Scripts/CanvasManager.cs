using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasManager : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Canvas;
    private bool pushFlag = false;  // ボタン押しすぎない用
    public bool check = true;       // 条件分岐用のbool変数
    // Update is called once per frame
    void Update()
    {
        // Aボタンで開く
        if (Input.GetKey(KeyCode.A))
        {
            if (pushFlag == false)
            {
                pushFlag = true;
                if (!check)
                {
                    // 非表示
                    Canvas.SetActive(false);
                    check = true;
                }
                else
                {
                    // 表示
                    Canvas.SetActive(true);
                    if(Camera)
                    {
                        Canvas.transform.position = Camera.transform.position + Camera.transform.forward * 3.0f;
                        Quaternion CameraRot = Camera.transform.rotation;
                        CameraRot.x = 0f;   // Canvasが斜めにならないように調整
                        CameraRot.z = 0f;   // Canvasが斜めにならないように調整
                        Canvas.transform.rotation = CameraRot;
                        check = false;
                    }
                }
                
            }
        } else {
            pushFlag = false;
        }
    }
}